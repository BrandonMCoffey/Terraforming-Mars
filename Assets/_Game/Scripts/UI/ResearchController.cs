using System.Collections.Generic;
using Scripts.Data;
using Scripts.Enums;
using Scripts.UI.Displays;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class ResearchController : MonoBehaviour
    {
        [SerializeField] private int _researchCost = 4;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _currencyText;
        [SerializeField] private TextMeshProUGUI _ironText;
        [SerializeField] private TextMeshProUGUI _titaniumText;
        [SerializeField] private Image _playerBanner;
        [SerializeField] private List<ResearchDisplay> _patentDisplays = new List<ResearchDisplay>();

        private PatentCollection _collection;
        private PlayerData _playerData;

        private void OnEnable()
        {
            foreach (var display in _patentDisplays) {
                display.OnPurchase += PurchasePatent;
            }
        }

        private void OnDisable()
        {
            foreach (var display in _patentDisplays) {
                display.OnPurchase -= PurchasePatent;
            }
        }

        public void Setup(PlayerData playerData, PatentCollection collection)
        {
            _playerData = playerData;
            _collection = collection;
            if (_titleText != null) _titleText.text = "Research Phase (" + playerData.PlayerName + ")";
            var patents = collection.GetRandom(_patentDisplays.Count, false);
            for (var i = 0; i < _patentDisplays.Count; i++) {
                if (i >= patents.Count || patents[i] == null) {
                    _patentDisplays[i].Display(null);
                } else {
                    _patentDisplays[i].Display(patents[i]);
                }
            }
            _currencyText.text = playerData.GetResource(ResourceType.Credits).ToString();
            _ironText.text = playerData.GetResource(ResourceType.Iron).ToString();
            _titaniumText.text = playerData.GetResource(ResourceType.Titanium).ToString();
            _playerBanner.color = playerData.PlayerColor;
        }

        public void PurchasePatent(ResearchDisplay display)
        {
            if (display.PatentData == null) {
                Debug.Log("Null patent", gameObject);
                return;
            }
            bool success = _playerData.RemoveResource(ResourceType.Credits, _researchCost);
            if (!success) return;
            _playerData.AddPatent(display.PatentData);
            _collection.RemovePatent(display.PatentData);
            _currencyText.text = _playerData.GetResource(ResourceType.Credits).ToString();
            _ironText.text = _playerData.GetResource(ResourceType.Iron).ToString();
            _titaniumText.text = _playerData.GetResource(ResourceType.Titanium).ToString();
            display.Complete();
        }
    }
}