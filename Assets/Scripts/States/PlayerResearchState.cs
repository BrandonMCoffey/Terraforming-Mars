using System;

namespace Scripts.States
{
    public class PlayerResearchState : State
    {
        public static event Action EnterResearch;
        private static event Action OnFinishResearch;

        public bool _activated;

        public override void Enter()
        {
            _activated = false;
            OnFinishResearch += EndState;
        }

        public override void Tick()
        {
            if (_activated) return;
            _activated = true;
            EnterResearch?.Invoke();
        }

        public override void Exit()
        {
            OnFinishResearch -= EndState;
        }

        public static void FinishResearch()
        {
            OnFinishResearch?.Invoke();
        }

        private void EndState()
        {
            StateMachine.ChangeState<OpponentResearchState>();
        }
    }
}