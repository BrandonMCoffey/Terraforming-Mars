using Utility.StateMachine;

namespace Scripts.StateMachine
{
    public class TbState : State
    {
        protected TurnBasedStateMachine StateMachine { get; private set; }

        private void Awake()
        {
            StateMachine = GetComponent<TurnBasedStateMachine>();
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