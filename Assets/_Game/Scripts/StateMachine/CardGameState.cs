using Scripts.StateMachine.Base;
using UnityEngine;

namespace Scripts.StateMachine
{
    public class CardGameState : State
    {
        [SerializeField] private CardGameSM _stateMachine;
        protected CardGameSM StateMachine => _stateMachine;

        private void Awake()
        {
            if (_stateMachine == null) {
                _stateMachine = GetComponent<CardGameSM>();
            }
        }

        public override void Enter()
        {
        }

        public override void Tick()
        {
        }

        public override void Exit()
        {
        }
    }
}