using System;
using Scripts.CardGame.StateMachine;
using UnityEngine;

namespace Scripts.CardGame.States
{
    public class SetupCardGameState : CardGameState
    {
        [SerializeField] private int _startingCardNumber = 5;

        public static event Action ResetGame;

        private bool _activated;

        // NOTE: DO NOT CHANGE STATE IN ENTER OR EXIT. ONLY TICK

        public override void Enter()
        {
            _activated = false;
            ResetGame?.Invoke();
        }

        public override void Tick()
        {
            if (!_activated) {
                Debug.Log("Dealing " + _startingCardNumber + " cards to each player.");

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