using Scripts.CardGame.States;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class CardGameUIController : MonoBehaviour
    {
        [SerializeField] private Text _playerTurnText = null;
        [SerializeField] private Text _enemyTurnText = null;

        private void OnEnable()
        {
            EnemyTurnCardGameState.TurnBegin += OnEnemyTurnBegin;
            EnemyTurnCardGameState.TurnEnd += OnEnemyTurnEnd;
            PlayerTurnCardGameState.TurnBegin += OnPlayerTurnBegin;
            PlayerTurnCardGameState.TurnEnd += OnPlayerTurnEnd;
        }

        private void OnDisable()
        {
            EnemyTurnCardGameState.TurnBegin -= OnEnemyTurnBegin;
            EnemyTurnCardGameState.TurnEnd -= OnEnemyTurnEnd;
            PlayerTurnCardGameState.TurnBegin -= OnPlayerTurnBegin;
            PlayerTurnCardGameState.TurnEnd -= OnPlayerTurnEnd;
        }

        private void Start()
        {
            EnableUI(_playerTurnText, false);
            EnableUI(_enemyTurnText, false);
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

        private void EnableUI(Text text, bool enabled)
        {
            if (text != null) {
                text.gameObject.SetActive(enabled);
            }
        }
    }
}