using System;

namespace Scripts.States
{
    public class PlayerResearchState : State
    {
        public static event Action EnterResearch;
        private static event Action OnFinishResearch;

        public override void Enter()
        {
            EnterResearch?.Invoke();
            OnFinishResearch += EndState;
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