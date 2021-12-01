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
            _turnEndTime = Time.time + 5f;
            _playerData.StartTurn();
        }

        public override void Tick()
        {
            if (Time.time > _turnEndTime) {
                OnEndTurn();
            }
        }

        public override void Exit()
        {
            _playerData.EndTurn();
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