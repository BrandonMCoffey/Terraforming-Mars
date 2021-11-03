using System;
using Scripts.CardGame.StateMachine;

namespace Scripts.CardGame.States
{
    public class WinCardGameState : CardGameState
    {
        public static event Action WinGame;

        public override void Enter()
        {
            WinGame?.Invoke();
            StateMachine.Input.Confirm += OnConfirm;
        }

        public override void Exit()
        {
            StateMachine.Input.Confirm -= OnConfirm;
        }

        private void OnConfirm()
        {
            StateMachine.ChangeState<SetupCardGameState>();
        }
    }
}