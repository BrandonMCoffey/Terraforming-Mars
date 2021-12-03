using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Data;
using Scripts.Enums;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.UI.Awards
{
    public class AwardController : MonoBehaviour
    {
        public static AwardController Instance;

        [SerializeField] private int _maxAwards = 3;
        [SerializeField] private GameData _gameData;
        [SerializeField] private GameObject _fundingCostObj;
        [SerializeField] private TextMeshProUGUI _fundingCostText;

        public static event Action<AwardType> OnFundAward;

        public List<AwardType> FundedAwards { get; private set; }

        private int _awardsFunded;
        private int _fundingCost;

        private void Awake()
        {
            Instance = this;
            _awardsFunded = 0;
            FundedAwards = new List<AwardType>(_maxAwards);
        }

        private void OnEnable()
        {
            UpdateFundingCost();
        }

        private void UpdateFundingCost()
        {
            if (_awardsFunded >= _maxAwards) {
                _fundingCostObj.SetActive(false);
                return;
            }
            _fundingCost = _awardsFunded switch {
                0 => 8,
                1 => 14,
                2 => 20,
                _ => _fundingCost
            };
            _fundingCostText.text = _fundingCost.ToString();
        }

        public bool Fund(AwardType type, PlayerData player = null)
        {
            if (_awardsFunded >= _maxAwards || FundedAwards.Contains(type)) return false;
            if (player == null) {
                player = _gameData.CurrentPlayer;
                if (!player.UserControlled) {
                    Debug.Log("Warning: Player attempting to fund awards on ai turn");
                    return false;
                }
            }
            if (!player.RemoveResource(ResourceType.Credits, _fundingCost)) return false;
            FundedAwards.Add(type);
            OnFundAward?.Invoke(type);
            _awardsFunded++;
            UpdateFundingCost();
            AnnouncementController.Instance.MinorAnnouncement(player.PlayerName + " Funded the " + type + " Award", GetDescription(type));
            return true;
        }

        public static string GetDescription(AwardType type)
        {
            return "Will be awarded to the player who " + type switch {
                AwardType.Landlord   => "owns the most tiles",
                AwardType.Banker     => "has the highest credit production",
                AwardType.Scientist  => "owns the most scientific patents",
                AwardType.Thermalist => "owns the most heat tokens",
                AwardType.Miner      => "owns the most iron and titanium",
                _                    => ""
            };
        }

        public bool FundAny(PlayerData player)
        {
            int attempts = 0;
            while (attempts++ < 10) {
                var award = (AwardType) Random.Range(0, (int) Enum.GetValues(typeof(AwardType)).Cast<AwardType>().Max());
                if (Fund(award, player)) {
                    return true;
                }
            }
            return false;
        }
    }
}