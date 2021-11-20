using System;
using Scripts.StateMachine;

namespace Scripts.States
{
    public class SetupTbState : TbState
    {
        public static event Action ResetGame;

        private bool _activated;

        // NOTE: DO NOT CHANGE STATE IN ENTER OR EXIT. ONLY TICK

        public override void Enter()
        {
            _activated = false;
            ResetGame?.Invoke();
        }

        public override void Tick()
        {
            if (!_activated) {
                _activated = true;
                StateMachine.ChangeState<PlayerTurnTbState>();
            }
        }

        public override void Exit()
        {
            _activated = false;
        }
    }
}