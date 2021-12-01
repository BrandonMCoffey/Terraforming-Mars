using Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class MatchPlayerInfo : MonoBehaviour
    {
        [SerializeField] private GameData _gameData = null;
        [SerializeField] private TextMeshProUGUI _player1Name = null;
        [SerializeField] private Image _player1Color = null;
        [SerializeField] private TextMeshProUGUI _player2Name = null;
        [SerializeField] private Image _player2Color = null;

        private void Start()
        {
            if (_gameData == null) return;
            if (_player1Name != null) _player1Name.text = _gameData.Player.PlayerName;
            if (_player1Color != null) _player1Color.color = _gameData.Player.PlayerColor;
            if (_player2Name != null) _player2Name.text = _gameData.Opponent.PlayerName;
            if (_player2Color != null) _player2Color.color = _gameData.Opponent.PlayerColor;
        }
    }
}