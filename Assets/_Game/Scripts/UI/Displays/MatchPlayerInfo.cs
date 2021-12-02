using Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Displays
{
    public class MatchPlayerInfo : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;
        [SerializeField] private TextMeshProUGUI _player1Name;
        [SerializeField] private Image _player1Color;
        [SerializeField] private TextMeshProUGUI _player2Name;
        [SerializeField] private Image _player2Color;
        [SerializeField] private float _colorAlpha = 1;

        private void OnEnable()
        {
            if (_gameData == null) return;
            // Player 1
            if (_player1Name != null) _player1Name.text = _gameData.Player.PlayerName;
            if (_player1Color != null) {
                Color player1Color = _gameData.Player.PlayerColor;
                player1Color.a = _colorAlpha;
                _player1Color.color = player1Color;
            }
            // Player 2
            if (_player2Name != null) _player2Name.text = _gameData.Opponent.PlayerName;
            if (_player2Color != null) {
                Color player2Color = _gameData.Opponent.PlayerColor;
                player2Color.a = _colorAlpha;
                _player2Color.color = player2Color;
            }
        }
    }
}