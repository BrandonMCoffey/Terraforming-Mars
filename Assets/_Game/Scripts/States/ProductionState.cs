using System;
using UnityEngine;

namespace Scripts.States
{
    public class ProductionState : State
    {
        public static event Action EnterProduction;
        public static event Action OnFinishProduction;

        private bool _autoEndProduction;

        public override void Enter()
        {
            if (!StateMachine.Player.UserControlled && !StateMachine.Opponent.UserControlled) {
                _autoEndProduction = true;
            } else {
                EnterProduction?.Invoke();
            }
            OnFinishProduction += EndState;
        }

        public override void Tick()
        {
            if (_autoEndProduction) {
                OnFinishProduction?.Invoke();
                _autoEndProduction = false;
            }
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