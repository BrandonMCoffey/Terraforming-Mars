using Scripts.States;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class TurnBasedGameUIController : MonoBehaviour
    {
        [SerializeField] private GameObject _playerTurn = null;
        [SerializeField] private Text _playerTurnText = null;
        [SerializeField] private GameObject _enemyTurn = null;
        [SerializeField] private Text _enemyTurnText = null;
        [SerializeField] private GameObject _winGame = null;
        [SerializeField] private GameObject _loseGame = null;

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
            EnableUI(_playerTurn, false);
            EnableUI(_enemyTurn, false);
            EnableUI(_winGame, false);
            EnableUI(_loseGame, false);
        }

        private void OnPlayerTurnBegin(int turnNumber)
        {
            EnableUI(_playerTurn, true);
            if (_playerTurnText != null) {
                _playerTurnText.text = turnNumber.ToString();
            }
        }

        private void OnPlayerTurnEnd()
        {
            EnableUI(_playerTurn, false);
        }

        private void OnEnemyTurnBegin(int turnNumber)
        {
            EnableUI(_enemyTurn, true);
            if (_enemyTurnText != null) {
                _enemyTurnText.text = turnNumber.ToString();
            }
        }

        private void OnEnemyTurnEnd()
        {
            EnableUI(_enemyTurn, false);
        }

        private void OnWinGame()
        {
            EnableUI(_winGame, true);
        }

        private void OnLoseGame()
        {
            EnableUI(_loseGame, true);
        }

        private static void EnableUI(Text text, bool enabled)
        {
            if (text != null) {
                text.gameObject.SetActive(enabled);
            }
        }

        private static void EnableUI(GameObject obj, bool enabled)
        {
            if (obj != null) {
                obj.gameObject.SetActive(enabled);
            }
        }
    }
}