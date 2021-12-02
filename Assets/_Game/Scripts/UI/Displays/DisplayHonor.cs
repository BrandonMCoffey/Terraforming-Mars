using Scripts.Data;
using TMPro;
using UnityEngine;

namespace Scripts.UI.Displays
{
    public class DisplayHonor : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;
        [SerializeField] private bool _player1;
        [SerializeField] private TextMeshProUGUI _text;

        private void Start()
        {
            if (_gameData == null || _text == null) return;
            if (_player1) {
                _gameData.Player.OnHonorChanged += UpdateDisplay;
            } else {
                _gameData.Opponent.OnHonorChanged += UpdateDisplay;
            }
        }

        private void UpdateDisplay()
        {
            _text.text = _player1 ? _gameData.Player.Honor.ToString() : _gameData.Opponent.Honor.ToString();
        }
    }
}