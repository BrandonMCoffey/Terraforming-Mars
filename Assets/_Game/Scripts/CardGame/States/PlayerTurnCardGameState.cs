using System;
using Scripts.CardGame.StateMachine;

namespace Scripts.CardGame.States
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
        }

        public override void Exit()
        {
            TurnEnd?.Invoke();

            StateMachine.Input.Confirm -= OnConfirm;
        }

        private void OnConfirm()
        {
            StateMachine.ChangeState<EnemyTurnCardGameState>();
        }
    }
}