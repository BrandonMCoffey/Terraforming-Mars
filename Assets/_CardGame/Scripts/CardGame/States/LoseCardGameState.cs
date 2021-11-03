using System;
using _CardGame.Scripts.CardGame.StateMachine;

namespace _CardGame.Scripts.CardGame.States
{
    public class LoseCardGameState : CardGameState
    {
        public static event Action LoseGame;

        public override void Enter()
        {
            LoseGame?.Invoke();
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