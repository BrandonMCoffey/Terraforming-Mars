namespace Scripts.States
{
    public class SetupState : State
    {
        private bool _activated;

        public override void Enter()
        {
            _activated = false;
            StateMachine.PatentCollection.RestoreList();
            StateMachine.PlayerData.SetupPlayer(StateMachine.PatentCollection);
            StateMachine.OpponentData.SetupPlayer(StateMachine.PatentCollection);
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