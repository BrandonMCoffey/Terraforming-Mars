using Scripts.Data;
using UnityEngine;

namespace Scripts.States
{
    public class SetupState : State
    {
        [SerializeField] private PlayerData _playerData = null;
        [SerializeField] [Range(0, 100)] private int _startingCredits = 20;

        private bool _activated;

        public override void Enter()
        {
            _playerData.SetCredits(_startingCredits);
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