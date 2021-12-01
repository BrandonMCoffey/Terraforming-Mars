using System.Collections.Generic;

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
            var turns = new List<State>(2);
            if (StateMachine.Player.UserControlled) {
                var playerTurn = gameObject.AddComponent<PlayerTurnState>();
                playerTurn.Setup(StateMachine.Player);
                turns.Add(playerTurn);
            } else {
                var playerTurn = gameObject.AddComponent<AiTurnState>();
                playerTurn.Setup(StateMachine.Player);
                turns.Add(playerTurn);
            }
            if (StateMachine.Opponent.UserControlled) {
                var opponentTurn = gameObject.AddComponent<PlayerTurnState>();
                opponentTurn.Setup(StateMachine.Opponent);
                turns.Add(opponentTurn);
            } else {
                var opponentTurn = gameObject.AddComponent<AiTurnState>();
                opponentTurn.Setup(StateMachine.Opponent);
                turns.Add(opponentTurn);
            }
            StateMachine.SetupTurns(turns);
        }

        public override void Tick()
        {
            if (_activated) return;
            _activated = true;
            StateMachine.NextTurn();
        }

        public override void Exit()
        {
        }
    }
}