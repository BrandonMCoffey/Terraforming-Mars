using Utility.StateMachine;

namespace Scripts.States
{
    public class State : StateBase
    {
        protected MasterStateMachine StateMachine { get; private set; }

        private void Awake()
        {
            StateMachine = GetComponent<MasterStateMachine>();
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