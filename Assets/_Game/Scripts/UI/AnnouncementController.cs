using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scripts.Data;
using Scripts.Enums;
using Scripts.States;
using Scripts.UI.Awards;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility.Buttons;
using Random = UnityEngine.Random;

namespace Scripts.UI
{
    public class AnnouncementController : MonoBehaviour
    {
        public static AnnouncementController Instance;

        [Header("Settings")]
        [SerializeField] private float _waitTime = 0.25f;
        [SerializeField] private AnimationCurve _popupTiming = AnimationCurve.Linear(0, 0, 1, 1);
        [SerializeField] private float _holdTime = 2f;
        [SerializeField] private AnimationCurve _closeTiming = AnimationCurve.Linear(0, 1, 1, 0);

        [Header("References")]
        [SerializeField] private GameData _gameData;
        [SerializeField] private GameObject _raycastBlock;

        [Header("Announcement Banners")]
        [SerializeField] private RectMask2D _bannerMask;
        [SerializeField] private float _bannerHeight = 125;
        [SerializeField] private TextMeshProUGUI _bannerTitle;
        [SerializeField] private TextMeshProUGUI _bannerSubtitle;
        [SerializeField] private GameObject _bannerQuitGameButton;
        [SerializeField] private RectMask2D _smallBannerMask;
        [SerializeField] private float _smallBannerHeight = 75;
        [SerializeField] private TextMeshProUGUI _smallBannerTitle;
        [SerializeField] private TextMeshProUGUI _smallBannerSubtitle;

        [Header("Production and Research")]
        [SerializeField] private RectMask2D _productionMask;
        [SerializeField] private float _productionHeight = 290;
        [SerializeField] private RectMask2D _researchMask;
        [SerializeField] private float _researchHeight = 290;
        [SerializeField] private ResearchController _researchController;

        // True when no announcements left. False when still has announcements
        public static event Action<bool> OnAnnouncementUpdate;

        private Queue<IEnumerator> _announcements = new Queue<IEnumerator>();
        private Coroutine _currentRoutine;
        private bool _hold;
        private bool _player1Research;
        private List<AwardType> _fundedAwards;
        private bool _gameOver;

        #region Unity Functions

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _raycastBlock.SetActive(false);
            _bannerMask.gameObject.SetActive(false);
            _productionMask.gameObject.SetActive(false);
            _smallBannerMask.gameObject.SetActive(false);
            _fundedAwards = new List<AwardType>();
        }

        private void OnEnable()
        {
            if (_gameData == null) return;
            AwardController.OnFundAward += FundAward;
            _gameData.Player.OnTurnStart += AnnouncePlayer1;
            _gameData.Opponent.OnTurnStart += AnnouncePlayer2;
            ProductionState.EnterProduction += AnnounceProduction;
            PlayerResearchState.EnterResearch += AnnouncePlayerResearch;
            OpponentResearchState.EnterResearch += AnnounceOpponentResearch;
            MasterStateMachine.ForceCloseAnnouncements += ForceCloseAnnouncements;
        }

        private void OnDisable()
        {
            if (_gameData == null) return;
            AwardController.OnFundAward -= FundAward;
            _gameData.Player.OnTurnStart -= AnnouncePlayer1;
            _gameData.Opponent.OnTurnStart -= AnnouncePlayer2;
            ProductionState.EnterProduction -= AnnounceProduction;
            PlayerResearchState.EnterResearch -= AnnouncePlayerResearch;
            OpponentResearchState.EnterResearch -= AnnounceOpponentResearch;
            MasterStateMachine.ForceCloseAnnouncements -= ForceCloseAnnouncements;
        }

        #endregion

        #region Game Over

        private const float GameOverWaitTime = 0.1f;
        private const float GameOverAnnouncementTime = 3f;

        private void FundAward(AwardType type)
        {
            _fundedAwards.Add(type);
        }


        public void TerraformingStatusComplete(PlanetStatusType type)
        {
            string title = "After " + _gameData.Generation + " Generations, " + _gameData.Planet.PlanetName + " is sustainable by " + type switch {
                PlanetStatusType.Oxygen        => "Oxygen Level",
                PlanetStatusType.Heat          => "Temperature",
                PlanetStatusType.Water         => "Water Level",
                PlanetStatusType.MagneticField => "Its Magnetic Field",
                _                              => ""
            };
            _announcements.Enqueue(AnnounceRoutine(title, "", _bannerMask, _bannerHeight, _waitTime * 2f, _holdTime));
            CheckRoutine();
        }

        public void GameOver()
        {
            _gameOver = true;
            _raycastBlock.SetActive(true);
            _announcements.Enqueue(AnnounceRoutine("After " + _gameData.Generation + " Generations, " + _gameData.Planet.PlanetName + " has been successfully Terraformed", "", _bannerMask, _bannerHeight, GameOverWaitTime, GameOverAnnouncementTime));

            // Display Bonus From Cities
            int neighboringForests = _gameData.Player.OwnedCities.Sum(city => city.GetNeighbors(TileType.Forest));
            if (neighboringForests > 0) {
                _announcements.Enqueue(AnnounceRoutine(_gameData.Player.PlayerName + " gets +" + neighboringForests + " Honor", "One for each forest next to their owned cities.", _bannerMask, _bannerHeight, GameOverWaitTime, GameOverAnnouncementTime));
            }
            neighboringForests = _gameData.Opponent.OwnedCities.Sum(city => city.GetNeighbors(TileType.Forest));
            if (neighboringForests > 0) {
                _announcements.Enqueue(AnnounceRoutine(_gameData.Opponent.PlayerName + " gets +" + neighboringForests + " Honor", "One for each forest next to their owned cities.", _bannerMask, _bannerHeight, GameOverWaitTime, GameOverAnnouncementTime));
            }

            // Display Milestones
            foreach (var milestone in _gameData.Player.Milestones) {
                _announcements.Enqueue(AnnounceRoutine(_gameData.Player.PlayerName + " is the " + milestone, GetDescription(milestone), _bannerMask, _bannerHeight, GameOverWaitTime, GameOverAnnouncementTime, false, null, _gameData.Player));
            }
            foreach (var milestone in _gameData.Opponent.Milestones) {
                _announcements.Enqueue(AnnounceRoutine(_gameData.Player.PlayerName + " is the " + milestone, GetDescription(milestone), _bannerMask, _bannerHeight, GameOverWaitTime, GameOverAnnouncementTime, false, null, _gameData.Opponent));
            }
            // Display Funded Awards (And winners)
            foreach (var award in _fundedAwards) {
                AwardWinner(award);
            }
            CheckRoutine();
        }

        public static string GetDescription(MilestoneType type)
        {
            return type switch {
                MilestoneType.Terraformer => "Claimed for being a quick rising star with 35 Honor early on. +5 Honor",
                MilestoneType.Mayor       => "Claimed for focusing on building cities. +5 Honor",
                MilestoneType.Gardener    => "Claimed for focusing on forests and plants. +5 Honor",
                MilestoneType.Builder     => "Claimed for building as much as possible. +5 Honor",
                MilestoneType.Planner     => "Claimed for signing on to too many patents. +5 Honor",
                _                         => ""
            };
        }

        public static string GetDescription(AwardType type)
        {
            return type switch {
                AwardType.Landlord   => "Awarded for owning the most tiles. +5 Honor",
                AwardType.Banker     => "Awarded for having the highest currency production. +5 Honor",
                AwardType.Scientist  => "Awarded for having the most scientific credit. +5 Honor",
                AwardType.Thermalist => "Awarded for owning the most heat tokens. +5 Honor",
                AwardType.Miner      => "Awarded for owning the most iron and steel tokens. +5 Honor",
                _                    => ""
            };
        }

        private void AwardWinner(AwardType type)
        {
            int player;
            int opponent;
            switch (type) {
                case AwardType.Landlord:
                    player = _gameData.Player.OwnedTiles;
                    opponent = _gameData.Opponent.OwnedTiles;
                    if (player >= opponent) {
                        _announcements.Enqueue(AnnounceRoutine(_gameData.Player.PlayerName + " earned the " + type + " Award", GetDescription(type), _bannerMask, _bannerHeight, GameOverWaitTime, GameOverAnnouncementTime, false, null, _gameData.Player));
                    }
                    if (opponent >= player) {
                        _announcements.Enqueue(AnnounceRoutine(_gameData.Opponent.PlayerName + " earned the " + type + " Award", GetDescription(type), _bannerMask, _bannerHeight, GameOverWaitTime, GameOverAnnouncementTime, false, null, _gameData.Opponent));
                    }
                    break;
                case AwardType.Banker:
                    player = _gameData.Player.GetResource(ResourceType.Credits, true);
                    opponent = _gameData.Opponent.GetResource(ResourceType.Credits, true);
                    if (player >= opponent) {
                        _announcements.Enqueue(AnnounceRoutine(_gameData.Player.PlayerName + " earned the " + type + " Award", GetDescription(type), _bannerMask, _bannerHeight, GameOverWaitTime, GameOverAnnouncementTime, false, null, _gameData.Player));
                    }
                    if (opponent >= player) {
                        _announcements.Enqueue(AnnounceRoutine(_gameData.Opponent.PlayerName + " earned the " + type + " Award", GetDescription(type), _bannerMask, _bannerHeight, GameOverWaitTime, GameOverAnnouncementTime, false, null, _gameData.Opponent));
                    }
                    break;
                case AwardType.Scientist:
                    break;
                case AwardType.Thermalist:
                    player = _gameData.Player.GetResource(ResourceType.Heat);
                    opponent = _gameData.Opponent.GetResource(ResourceType.Heat);
                    if (player >= opponent) {
                        _announcements.Enqueue(AnnounceRoutine(_gameData.Player.PlayerName + " earned the " + type + " Award", GetDescription(type), _bannerMask, _bannerHeight, GameOverWaitTime, GameOverAnnouncementTime, false, null, _gameData.Player));
                    }
                    if (opponent >= player) {
                        _announcements.Enqueue(AnnounceRoutine(_gameData.Opponent.PlayerName + " earned the " + type + " Award", GetDescription(type), _bannerMask, _bannerHeight, GameOverWaitTime, GameOverAnnouncementTime, false, null, _gameData.Opponent));
                    }
                    break;
                case AwardType.Miner:
                    player = _gameData.Player.GetResource(ResourceType.Iron) + _gameData.Player.GetResource(ResourceType.Titanium);
                    opponent = _gameData.Opponent.GetResource(ResourceType.Iron) + _gameData.Opponent.GetResource(ResourceType.Titanium);
                    if (player >= opponent) {
                        _announcements.Enqueue(AnnounceRoutine(_gameData.Player.PlayerName + " earned the " + type + " Award", GetDescription(type), _bannerMask, _bannerHeight, GameOverWaitTime, GameOverAnnouncementTime, false, null, _gameData.Player));
                    }
                    if (opponent >= player) {
                        _announcements.Enqueue(AnnounceRoutine(_gameData.Opponent.PlayerName + " earned the " + type + " Award", GetDescription(type), _bannerMask, _bannerHeight, GameOverWaitTime, GameOverAnnouncementTime, false, null, _gameData.Opponent));
                    }
                    break;
            }
            _announcements.Enqueue(AnnounceRoutine("", "", _bannerMask, _bannerHeight, _waitTime, 100, true, null, null, true));

            CheckRoutine();
        }

        #endregion

        #region Banner Announcements

        private void AnnouncePlayer1()
        {
            Announce(_gameData.Player.PlayerName + "'s Turn!", MilestoneNotification(_gameData.Player));
        }

        private void AnnouncePlayer2()
        {
            Announce(_gameData.Opponent.PlayerName + "'s Turn!", "");
        }

        private static string MilestoneNotification(PlayerData player)
        {
            if (!player.UserControlled) return "";
            return MilestoneController.CanClaimAny(player) ? "New Milestone Available, Ready to be Claimed." : "";
        }

        [Button(Spacing = 10)]
        public void Announce(string title, string subtitle)
        {
            Announce(title, subtitle, _waitTime, _holdTime);
        }

        public void Announce(string title, string subtitle, float waitTime, float holdTime)
        {
            if (_gameOver) return;
            _announcements.Enqueue(AnnounceRoutine(title, subtitle, _bannerMask, _bannerHeight, waitTime, holdTime));
            CheckRoutine();
        }

        public void MinorAnnouncement(string title, string subtitle)
        {
            if (_gameOver) return;
            _announcements.Enqueue(AnnounceRoutine(title, subtitle, _smallBannerMask, _smallBannerHeight, 0, _holdTime * 2f));
            CheckRoutine();
        }

        #endregion

        #region Production and Research Announcements

        private void AnnounceProduction()
        {
            if (_gameOver) return;
            _announcements.Enqueue(AnnounceRoutine("Generation Over", "Preparing for production and research until the next generation.", _bannerMask, _bannerHeight, _waitTime, _holdTime, false));
            _announcements.Enqueue(AnnounceRoutine("", "", _productionMask, _productionHeight, _waitTime, _holdTime, true));
            CheckRoutine();
            ProductionState.OnFinishProduction += EndProduction;
        }

        public void EndProduction()
        {
            ProductionState.OnFinishProduction -= EndProduction;
            ProductionState.FinishProduction();
            _hold = false;
        }

        private void AnnouncePlayerResearch()
        {
            if (_gameOver) return;
            _player1Research = true;
            if (!_gameData.Player.UserControlled) {
                AiResearch(_gameData.Player);
                PlayerResearchState.FinishResearch();
                return;
            }
            _announcements.Enqueue(AnnounceRoutine("", "", _researchMask, _researchHeight, _waitTime, _holdTime, true, _gameData.Player));
            CheckRoutine();
        }

        private void AnnounceOpponentResearch()
        {
            if (_gameOver) return;
            _player1Research = false;
            if (!_gameData.Opponent.UserControlled) {
                AiResearch(_gameData.Opponent);
                OpponentResearchState.FinishResearch();
                return;
            }
            _announcements.Enqueue(AnnounceRoutine("", "", _researchMask, _researchHeight, _waitTime, _holdTime, true, _gameData.Opponent));
            CheckRoutine();
        }

        private void AiResearch(PlayerData player)
        {
            int rand = Random.Range(-1, 3);
            while (rand > 0) {
                if (player.RemoveResource(ResourceType.Credits, 4)) {
                    player.AddPatent(_gameData.PatentCollection.GetRandom());
                } else {
                    break;
                }
                rand--;
            }
        }

        public void EndResearch()
        {
            if (_player1Research) {
                PlayerResearchState.FinishResearch();
            } else {
                OpponentResearchState.FinishResearch();
            }
            _hold = false;
        }

        #endregion

        #region Announcement Routine

        private void ForceCloseAnnouncements()
        {
            if (_currentRoutine == null) return;
            _announcements.Clear();
            StopCoroutine(_currentRoutine);
            _currentRoutine = null;
            _bannerMask.gameObject.SetActive(false);
            _productionMask.gameObject.SetActive(false);
            _researchMask.gameObject.SetActive(false);
            _smallBannerMask.gameObject.SetActive(false);
        }

        private void CheckRoutine()
        {
            if (_currentRoutine != null || _announcements.Count == 0) return;
            _currentRoutine = StartCoroutine(_announcements.Dequeue());
        }

        private IEnumerator AnnounceRoutine(string title, string subtitle, RectMask2D mask, float height, float wait, float hold, bool longHold = false, PlayerData researchData = null, PlayerData honorBonusPlayer = null, bool winnerCheck = false, int honorBonusAmount = 5)
        {
            OnAnnouncementUpdate?.Invoke(false);
            _bannerMask.gameObject.SetActive(false);
            _productionMask.gameObject.SetActive(false);
            _researchMask.gameObject.SetActive(false);
            _smallBannerMask.gameObject.SetActive(false);
            mask.gameObject.SetActive(true);
            if (winnerCheck) {
                int playerHonor = _gameData.Player.Honor;
                int opponentHonor = _gameData.Opponent.Honor;
                if (playerHonor == opponentHonor) {
                    // TIE
                    _bannerTitle.text = "It's a Tie! Good Game!";
                } else if (playerHonor > opponentHonor) {
                    // PLAYER WINS
                    _bannerTitle.text = _gameData.Player.PlayerName + " Wins the Game!";
                } else {
                    // OPPONENT WINS
                    _bannerTitle.text = _gameData.Opponent.PlayerName + " Wins the Game!";
                }
                _bannerSubtitle.text = "";
                _bannerQuitGameButton.SetActive(true);
                _hold = true;
                longHold = true;
            } else {
                _bannerTitle.text = title;
                _bannerSubtitle.text = subtitle;
                _smallBannerTitle.text = title;
                _smallBannerSubtitle.text = subtitle;
            }
            if (!_gameOver && mask != _smallBannerMask) {
                _raycastBlock.SetActive(true);
            }
            SetBannerHeight(mask, height);
            for (float t = 0; t < wait; t += Time.deltaTime) {
                yield return null;
            }
            if (researchData != null) {
                _researchController.Setup(researchData, _gameData.PatentCollection);
            }
            float start = _popupTiming.keys[0].time;
            float end = _popupTiming.keys[_popupTiming.length - 1].time;
            for (float t = start; t < end; t += Time.deltaTime) {
                float value = Mathf.Clamp01(_popupTiming.Evaluate(t));
                SetBannerHeight(mask, height - value * height);
                yield return null;
            }
            SetBannerHeight(mask, 0);
            if (longHold) {
                _hold = true;
                while (_hold) {
                    yield return null;
                }
            } else {
                for (float t = 0; t < hold; t += Time.deltaTime) {
                    yield return null;
                }
            }
            if (honorBonusPlayer != null) {
                honorBonusPlayer.AddHonor(honorBonusAmount);
            }
            start = _closeTiming.keys[0].time;
            end = _closeTiming.keys[_closeTiming.length - 1].time;
            for (float t = start; t < end; t += Time.deltaTime) {
                float value = Mathf.Clamp01(_closeTiming.Evaluate(t));
                SetBannerHeight(mask, height - value * height);
                yield return null;
            }
            SetBannerHeight(mask, height);
            mask.gameObject.SetActive(false);
            if (!_gameOver) {
                _raycastBlock.SetActive(false);
            }
            _currentRoutine = null;
            OnAnnouncementUpdate?.Invoke(_announcements.Count == 0);
            CheckRoutine();
        }

        private static void SetBannerHeight(RectMask2D mask, float height)
        {
            var padding = mask.padding;
            padding.w = height; // Top Padding
            padding.y = height; // Bottom Padding
            mask.padding = padding;
        }

        #endregion
    }
}