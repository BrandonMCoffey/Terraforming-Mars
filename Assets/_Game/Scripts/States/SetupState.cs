namespace Scripts.States
{
    public class SetupState : State
    {
        private bool _activated;

        public override void Enter()
        {
            _activated = false;
        }

        public override void Tick()
        {
            if (!_activated) {
                _activated = true;
                StateMachine.ChangeState<PlayerTurnState>();
            }
        }

        public override void Exit()
        {
        }
    }
}