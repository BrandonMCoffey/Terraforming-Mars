namespace Scripts.States
{
    public class SetupState : State
    {
        private bool _activated;

        public override void Enter()
        {
            _activated = false;
            StateMachine.PatentCollection.RestoreList();
            StateMachine.Player.SetupPlayer(StateMachine.PatentCollection);
            StateMachine.Opponent.SetupPlayer(StateMachine.PatentCollection);
            StateMachine.Planet.Setup();
        }

        public override void Tick()
        {
            if (_activated) return;
            _activated = true;
            StateMachine.ChangeState<PlayerTurnState>();
        }

        public override void Exit()
        {
        }
    }
}