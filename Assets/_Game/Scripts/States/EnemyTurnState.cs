using UnityEngine;

namespace Scripts.States
{
    public class EnemyTurnState : State
    {
        private float _turnEndTime;

        public override void Enter()
        {
            _turnEndTime = Time.time + 2;
        }

        public override void Tick()
        {
            if (Time.time > _turnEndTime) {
                OnEndTurn();
            }
        }

        public override void Exit()
        {
        }

        private void OnEndTurn()
        {
            StateMachine.ChangeState<PlayerTurnState>();
        }
    }
}