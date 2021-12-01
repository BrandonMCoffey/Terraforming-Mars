using Scripts.Data;
using Scripts.Enums;
using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class DisplayResource : MonoBehaviour
    {
        [SerializeField] private ResourceType _type = ResourceType.Credits;
        [SerializeField] private GameData _gameData = null;
        [SerializeField] private bool _player = true;
        [SerializeField] private TextMeshProUGUI _text = null;

        private void OnEnable()
        {
            if (_gameData == null || _text == null) return;
            if (_player) {
                _gameData.Player.OnResourcesChanged += UpdateDisplay;
            } else {
                _gameData.Opponent.OnResourcesChanged += UpdateDisplay;
            }
            UpdateDisplay();
        }

        private void OnDisable()
        {
            if (_gameData == null || _text == null) return;
            if (_player) {
                _gameData.Player.OnResourcesChanged -= UpdateDisplay;
            } else {
                _gameData.Opponent.OnResourcesChanged -= UpdateDisplay;
            }
        }

        private void UpdateDisplay()
        {
            if (_player) {
                _text.text = _gameData.Player.GetResource(_type).ToString();
            } else {
                _text.text = _gameData.Opponent.GetResource(_type).ToString();
            }
        }
    }
}