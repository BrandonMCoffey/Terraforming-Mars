using Scripts.CardGame;
using Scripts.Input;
using Scripts.StateMachine.Base;
using UnityEngine;

namespace Scripts.StateMachine
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