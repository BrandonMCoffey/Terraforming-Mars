using System;
using Scripts.Data;
using Scripts.Enums;
using UnityEngine;

namespace Scripts.Helper
{
    public class ShowOnPlayerTurn : MonoBehaviour
    {
        [SerializeField] private GameData _gameData = null;
        [SerializeField] private PlayerTypes _whichPlayer = PlayerTypes.Player1;
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
            if (_gameData.PlayerActive(_whichPlayer)) {
                _gameData.Player.OnTurnStart += ShowObject;
                _gameData.Player.OnTurnEnd += HideObject;
            }
            if (_gameData.OpponentActive(_whichPlayer)) {
                _gameData.Opponent.OnTurnStart += ShowObject;
                _gameData.Opponent.OnTurnEnd += HideObject;
            }
        }

        private void OnDisable()
        {
            if (_gameData == null) return;
            bool player1 = false;
            bool player2 = false;
            switch (_whichPlayer) {
                case PlayerTypes.Player1:
                    player1 = true;
                    break;
                case PlayerTypes.Player2:
                    player2 = true;
                    break;
                case PlayerTypes.AnyUser:
                    if (_gameData.Player.UserControlled) player1 = true;
                    if (_gameData.Opponent.UserControlled) player2 = true;
                    break;
                case PlayerTypes.AnyAi:
                    if (!_gameData.Player.UserControlled) player1 = true;
                    if (!_gameData.Opponent.UserControlled) player2 = true;
                    break;
            }
            if (player1) {
                _gameData.Player.OnTurnStart -= ShowObject;
                _gameData.Player.OnTurnEnd -= HideObject;
            }
            if (player2) {
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