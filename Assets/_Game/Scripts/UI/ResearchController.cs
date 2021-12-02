using System.Collections.Generic;
using Scripts.Data;
using Scripts.UI.Displays;
using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class ResearchController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private List<ResearchDisplay> _patentDisplays = new List<ResearchDisplay>();

        private PlayerData _playerData;

        public void Setup(PlayerData playerData, PatentCollection collection)
        {
            _playerData = playerData;
            if (_titleText != null) _titleText.text = "Research Phase (" + playerData.PlayerName + ")";
            var patents = collection.GetRandom(_patentDisplays.Count, false);
            for (var i = 0; i < _patentDisplays.Count; i++) {
                _patentDisplays[i].Display(patents[i]);
            }
        }
    }
}