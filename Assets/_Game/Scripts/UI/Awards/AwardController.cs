using System;
using System.Collections.Generic;
using Scripts.Data;
using Scripts.Enums;
using TMPro;
using UnityEngine;

namespace Scripts.UI.Awards
{
    public class AwardController : MonoBehaviour
    {
        [SerializeField] private int _maxAwards = 3;
        [SerializeField] private GameData _gameData;
        [SerializeField] private GameObject _fundingCostObj;
        [SerializeField] private TextMeshProUGUI _fundingCostText;

        public static event Action<AwardType> OnFundAward;

        private List<AwardType> _fundedAwards;

        private int _awardsFunded;
        private int _fundingCost;

        private void Awake()
        {
            _awardsFunded = 0;
            _fundedAwards = new List<AwardType>(_maxAwards);
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

        public bool Fund(AwardType type)
        {
            if (_awardsFunded >= _maxAwards || _fundedAwards.Contains(type)) return false;
            if (!_gameData.CurrentPlayer.RemoveResource(ResourceType.Credits, _fundingCost)) return false;
            _fundedAwards.Add(type);
            OnFundAward?.Invoke(type);
            _awardsFunded++;
            UpdateFundingCost();
            return true;
        }
    }
}