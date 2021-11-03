using Scripts.CardGame.States;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.CardGame.UI
{
    public class CardGameUIController : MonoBehaviour
    {
        [SerializeField] private Text _playerTurnText = null;
        [SerializeField] private Text _enemyTurnText = null;
        [SerializeField] private Text _winGameText = null;
        [SerializeField] private Text _loseGameText = null;

        private void OnEnable()
        {
            SetupCardGameState.ResetGame += OnResetGame;
            EnemyTurnCardGameState.TurnBegin += OnEnemyTurnBegin;
            EnemyTurnCardGameState.TurnEnd += OnEnemyTurnEnd;
            PlayerTurnCardGameState.TurnBegin += OnPlayerTurnBegin;
            PlayerTurnCardGameState.TurnEnd += OnPlayerTurnEnd;
            WinCardGameState.WinGame += OnWinGame;
            LoseCardGameState.LoseGame += OnLoseGame;
        }

        private void OnDisable()
        {
            SetupCardGameState.ResetGame -= OnResetGame;
            EnemyTurnCardGameState.TurnBegin -= OnEnemyTurnBegin;
            EnemyTurnCardGameState.TurnEnd -= OnEnemyTurnEnd;
            PlayerTurnCardGameState.TurnBegin -= OnPlayerTurnBegin;
            PlayerTurnCardGameState.TurnEnd -= OnPlayerTurnEnd;
            WinCardGameState.WinGame -= OnWinGame;
            LoseCardGameState.LoseGame -= OnLoseGame;
        }

        private void OnResetGame()
        {
            EnableUI(_playerTurnText, false);
            EnableUI(_enemyTurnText, false);
            EnableUI(_winGameText, false);
            EnableUI(_loseGameText, false);
        }

        private void OnPlayerTurnBegin(int turnNumber)
        {
            EnableUI(_playerTurnText, true);
            if (_playerTurnText != null) {
                _playerTurnText.text = "Player Turn: " + turnNumber;
            }
        }

        private void OnPlayerTurnEnd()
        {
            EnableUI(_playerTurnText, false);
        }

        private void OnEnemyTurnBegin()
        {
            EnableUI(_enemyTurnText, true);
        }

        private void OnEnemyTurnEnd()
        {
            EnableUI(_enemyTurnText, false);
        }

        private void OnWinGame()
        {
            EnableUI(_winGameText, true);
        }

        private void OnLoseGame()
        {
            EnableUI(_loseGameText, true);
        }

        private void EnableUI(Text text, bool enabled)
        {
            if (text != null) {
                text.gameObject.SetActive(enabled);
            }
        }
    }
}