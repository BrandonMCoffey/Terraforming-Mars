using Scripts.States;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class TurnBasedGameUIController : MonoBehaviour
    {
        [SerializeField] private Text _playerTurnText = null;
        [SerializeField] private Text _enemyTurnText = null;
        [SerializeField] private Text _winGameText = null;
        [SerializeField] private Text _loseGameText = null;

        private void OnEnable()
        {
            SetupTbState.ResetGame += OnResetGame;
            EnemyTurnTbState.TurnBegin += OnEnemyTurnBegin;
            EnemyTurnTbState.TurnEnd += OnEnemyTurnEnd;
            PlayerTurnTbState.TurnBegin += OnPlayerTurnBegin;
            PlayerTurnTbState.TurnEnd += OnPlayerTurnEnd;
            WinTbState.WinGame += OnWinGame;
            LoseTbState.LoseGame += OnLoseGame;
        }

        private void OnDisable()
        {
            SetupTbState.ResetGame -= OnResetGame;
            EnemyTurnTbState.TurnBegin -= OnEnemyTurnBegin;
            EnemyTurnTbState.TurnEnd -= OnEnemyTurnEnd;
            PlayerTurnTbState.TurnBegin -= OnPlayerTurnBegin;
            PlayerTurnTbState.TurnEnd -= OnPlayerTurnEnd;
            WinTbState.WinGame -= OnWinGame;
            LoseTbState.LoseGame -= OnLoseGame;
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