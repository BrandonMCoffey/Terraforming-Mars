using Scripts.StateMachine;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.CardGame
{
    public class PlayerTurnCardGameState : CardGameState
    {
        [SerializeField] private Text _playerTurnTextUI = null;

        private int _playerTurnCount;

        public override void Enter()
        {
            EnableUI(true);

            _playerTurnCount++;
            UpdateUI();
        }

        public override void Exit()
        {
            EnableUI(false);
        }

        private void EnableUI(bool enable)
        {
            if (_playerTurnTextUI != null) {
                _playerTurnTextUI.gameObject.SetActive(true);
            }
        }

        private void UpdateUI()
        {
            if (_playerTurnTextUI != null) {
                _playerTurnTextUI.text = "Player Turn: " + _playerTurnCount;
            }
        }
    }
}