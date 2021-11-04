using System;
using Scripts.StateMachine;
using UnityEngine;

namespace Scripts.States
{
    public class WinTbState : TbState
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
            StateMachine.ChangeState<SetupTbState>();
        }
    }
}