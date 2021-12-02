using System;

namespace Scripts.States
{
    public class ProductionState : State
    {
        public static event Action EnterProduction;
        private static event Action OnFinishProduction;

        public override void Enter()
        {
            EnterProduction?.Invoke();
            OnFinishProduction += EndState;
        }

        public override void Exit()
        {
            OnFinishProduction -= EndState;
        }

        public static void FinishProduction()
        {
            OnFinishProduction?.Invoke();
        }

        private void EndState()
        {
            StateMachine.Player.ProductionPhase();
            StateMachine.Opponent.ProductionPhase();
            StateMachine.ChangeState<PlayerResearchState>();
        }
    }
}