using Scripts.CardGame.States;
using Scripts.Input;
using Scripts.Utility.StateMachine;
using UnityEngine;

namespace Scripts.CardGame.StateMachine
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