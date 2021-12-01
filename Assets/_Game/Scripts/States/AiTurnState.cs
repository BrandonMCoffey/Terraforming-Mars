using Scripts.Data;
using UnityEngine;

namespace Scripts.States
{
    public class AiTurnState : State
    {
        private float _turnEndTime;

        // Reference Difficulty here
        private PlayerData _playerData;

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
            StateMachine.NextTurn();
        }

        public void Setup(PlayerData playerData)
        {
            _playerData = playerData;
        }
    }
}