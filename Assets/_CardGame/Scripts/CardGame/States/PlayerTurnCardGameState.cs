using System;
using _CardGame.Scripts.CardGame.StateMachine;

namespace _CardGame.Scripts.CardGame.States
{
    public class PlayerTurnCardGameState : CardGameState
    {
        public static event Action<int> TurnBegin;
        public static event Action TurnEnd;

        private int _playerTurnCount;

        public override void Enter()
        {
            TurnBegin?.Invoke(++_playerTurnCount);

            StateMachine.Input.Confirm += OnConfirm;
            StateMachine.Input.Cancel += OnCancel;
        }

        public override void Tick()
        {
            if (_playerTurnCount >= 5) {
                _playerTurnCount = 0;
                StateMachine.ChangeState<WinCardGameState>();
            }
        }

        public override void Exit()
        {
            TurnEnd?.Invoke();

            StateMachine.Input.Confirm -= OnConfirm;
            StateMachine.Input.Cancel -= OnCancel;
        }

        private void OnCancel()
        {
            StateMachine.ChangeState<LoseCardGameState>();
        }

        private void OnConfirm()
        {
            StateMachine.ChangeState<EnemyTurnCardGameState>();
        }
    }
}