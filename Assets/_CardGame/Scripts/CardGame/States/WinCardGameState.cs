using System;
using _CardGame.Scripts.CardGame.StateMachine;

namespace _CardGame.Scripts.CardGame.States
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