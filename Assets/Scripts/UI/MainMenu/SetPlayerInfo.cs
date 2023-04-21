using Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.MainMenu
{
    public class SetPlayerInfo : MonoBehaviour
    {
        [SerializeField] private PlayerData _player;

        [SerializeField] private Image _playerColor;
        [SerializeField] private TMP_InputField _playerText;

        public string GetName => _player.PlayerName;
        public Color GetColor => _playerColor.color;

        private void Start()
        {
            _playerText.SetTextWithoutNotify(_player.DefaultName);
            _playerColor.color = _player.DefaultColor;
            _player.PlayerName = _player.DefaultName;
            _player.PlayerColor = _player.DefaultColor;
        }

        public void SetName(string playerName)
        {
            _player.PlayerName = playerName;
            _playerText.SetTextWithoutNotify(playerName);
        }

        public void SetColor(Color playerColor)
        {
            _player.PlayerColor = playerColor;
            _playerColor.color = playerColor;
        }
    }
}