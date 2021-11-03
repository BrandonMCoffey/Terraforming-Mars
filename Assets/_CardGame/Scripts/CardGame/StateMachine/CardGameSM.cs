using _CardGame.Scripts.CardGame.States;
using Input;
using UnityEngine;
using Utility.StateMachine;

namespace _CardGame.Scripts.CardGame.StateMachine
{
    public class CardGameSM : StateMachineBase
    {
        [SerializeField] private InputController _input = null;
        public InputController Input => _input;

        private void Awake()
        {
            if (Input == null) {
                _input = FindObjectOfType<InputController>();
            }
        }

        private void Start()
        {
            ChangeState<SetupCardGameState>();
        }
    }
}