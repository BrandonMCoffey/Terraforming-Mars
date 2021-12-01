using Scripts.Data;
using Scripts.States;
using UnityEngine;

namespace Scripts.Helper
{
    public class ShowOnPlayerTurn : MonoBehaviour
    {
        [SerializeField] private GameData _gameData = null;
        [SerializeField] private bool _player1 = true;
        [SerializeField] private GameObject _obj;
        [SerializeField] private bool _invert = false;

        private void Start()
        {
            if (_obj == null) _obj = gameObject;
            HideObject();
        }

        private void OnEnable()
        {
            if (_gameData == null) return;
            if (_player1) {
                _gameData.Player.OnTurnStart += ShowObject;
                _gameData.Player.OnTurnEnd += HideObject;
            } else {
                _gameData.Opponent.OnTurnStart += ShowObject;
                _gameData.Opponent.OnTurnEnd += HideObject;
            }
        }

        private void OnDisable()
        {
            if (_gameData == null) return;
            if (_player1) {
                _gameData.Player.OnTurnStart -= ShowObject;
                _gameData.Player.OnTurnEnd -= HideObject;
            } else {
                _gameData.Opponent.OnTurnStart -= ShowObject;
                _gameData.Opponent.OnTurnEnd -= HideObject;
            }
        }

        private void ShowObject()
        {
            _obj.SetActive(!_invert);
        }

        private void HideObject()
        {
            _obj.SetActive(_invert);
        }
    }
}