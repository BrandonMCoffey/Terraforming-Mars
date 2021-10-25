using Scripts.CardGame.StateMachine;
using UnityEngine;

namespace Scripts.CardGame.States
{
    public class SetupCardGameState : CardGameState
    {
        [SerializeField] private int _startingCardNumber = 5;

        private bool _activated;

        // NOTE: DO NOT CHANGE STATE IN ENTER OR EXIT. ONLY TICK

        public override void Enter()
        {
            _activated = false;
        }

        public override void Tick()
        {
            if (!_activated) {
                _activated = true;
                StateMachine.ChangeState<PlayerTurnCardGameState>();
            }
        }

        public override void Exit()
        {
            _activated = false;
        }
    }
}