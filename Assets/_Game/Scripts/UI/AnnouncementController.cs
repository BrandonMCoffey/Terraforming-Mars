using System.Collections;
using System.Collections.Generic;
using Scripts.Data;
using Scripts.States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility.Buttons;

namespace Scripts.UI
{
    public class AnnouncementController : MonoBehaviour
    {
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

        [Header("Production and Research")]
        [SerializeField] private RectMask2D _productionMask;
        [SerializeField] private float _productionHeight = 290;
        [SerializeField] private RectMask2D _researchMask;
        [SerializeField] private float _researchHeight = 290;
        [SerializeField] private ResearchController _researchController;

        private Queue<IEnumerator> _announcements = new Queue<IEnumerator>();
        private Coroutine _currentRoutine;
        private bool _hold;
        private bool _player1Research;

        private void Start()
        {
            _raycastBlock.SetActive(false);
            _bannerMask.gameObject.SetActive(false);
            _productionMask.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            if (_gameData == null) return;
            _gameData.Player.OnTurnStart += AnnouncePlayer1;
            _gameData.Opponent.OnTurnStart += AnnouncePlayer2;
            ProductionState.EnterProduction += AnnounceProduction;
            PlayerResearchState.EnterResearch += AnnouncePlayerResearch;
            OpponentResearchState.EnterResearch += AnnounceOpponentResearch;
        }

        private void OnDisable()
        {
            if (_gameData == null) return;
            _gameData.Player.OnTurnStart -= AnnouncePlayer1;
            _gameData.Opponent.OnTurnStart -= AnnouncePlayer2;
            ProductionState.EnterProduction -= AnnounceProduction;
            PlayerResearchState.EnterResearch -= AnnouncePlayerResearch;
            OpponentResearchState.EnterResearch -= AnnounceOpponentResearch;
        }

        #region Banner Announcements

        private void AnnouncePlayer1()
        {
            Announce(_gameData.Player.PlayerName + "'s Turn!", "");
        }

        private void AnnouncePlayer2()
        {
            Announce(_gameData.Opponent.PlayerName + "'s Turn!", "");
        }

        [Button(Spacing = 10)]
        public void Announce(string title, string subtitle)
        {
            Announce(title, subtitle, _waitTime, _holdTime);
        }

        public void Announce(string title, string subtitle, float waitTime, float holdTime)
        {
            _bannerTitle.text = title;
            _bannerSubtitle.text = subtitle;
            _announcements.Enqueue(AnnounceRoutine(_bannerMask, _bannerHeight, waitTime, holdTime, false));
            CheckRoutine();
        }

        #endregion

        #region Production and Research Announcements

        private void AnnounceProduction()
        {
            _bannerTitle.text = "Generation Over";
            _bannerSubtitle.text = "Preparing for production and research until the next generation.";
            _announcements.Enqueue(AnnounceRoutine(_bannerMask, _bannerHeight, _waitTime, _holdTime, false));
            _announcements.Enqueue(AnnounceRoutine(_productionMask, _productionHeight, _waitTime, _holdTime, true));
            CheckRoutine();
        }

        private void AnnouncePlayerResearch()
        {
            _player1Research = true;
            _announcements.Enqueue(AnnounceRoutine(_researchMask, _researchHeight, _waitTime, _holdTime, true, _gameData.Player));
            CheckRoutine();
        }

        private void AnnounceOpponentResearch()
        {
            _player1Research = false;
            _announcements.Enqueue(AnnounceRoutine(_researchMask, _researchHeight, _waitTime, _holdTime, true, _gameData.Opponent));
            CheckRoutine();
        }

        public void EndProduction()
        {
            ProductionState.FinishProduction();
            _hold = false;
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

        private void CheckRoutine()
        {
            if (_currentRoutine != null || _announcements.Count == 0) return;
            _currentRoutine = StartCoroutine(_announcements.Dequeue());
        }

        private IEnumerator AnnounceRoutine(RectMask2D mask, float height, float wait, float hold, bool longHold, PlayerData researchData = null)
        {
            _raycastBlock.SetActive(true);
            _bannerMask.gameObject.SetActive(false);
            _productionMask.gameObject.SetActive(false);
            _researchMask.gameObject.SetActive(false);
            mask.gameObject.SetActive(true);
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
            start = _closeTiming.keys[0].time;
            end = _closeTiming.keys[_closeTiming.length - 1].time;
            for (float t = start; t < end; t += Time.deltaTime) {
                float value = Mathf.Clamp01(_closeTiming.Evaluate(t));
                SetBannerHeight(mask, height - value * height);
                yield return null;
            }
            SetBannerHeight(mask, height);
            mask.gameObject.SetActive(false);
            _raycastBlock.SetActive(false);
            _currentRoutine = null;
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