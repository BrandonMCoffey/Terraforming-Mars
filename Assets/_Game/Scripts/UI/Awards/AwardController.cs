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
            if (player == null) player = _gameData.CurrentPlayer;
            if (!player.RemoveResource(ResourceType.Credits, _fundingCost)) return false;
            FundedAwards.Add(type);
            OnFundAward?.Invoke(type);
            _awardsFunded++;
            UpdateFundingCost();
            return true;
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