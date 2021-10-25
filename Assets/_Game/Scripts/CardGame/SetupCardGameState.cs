using Scripts.StateMachine;
using UnityEngine;

namespace Scripts.CardGame
{
    public class SetupCardGameState : CardGameState
    {
        [SerializeField] private int _startingCardNumber = 10;
        [SerializeField] private int _numberOfPlayers = 2;

        public override void Enter()
        {
        }

        public override void Tick()
        {
        }

        public override void Exit()
        {
        }
    }
}