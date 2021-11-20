using Input;
using Scripts.States;
using UnityEngine;
using Utility.StateMachine;

namespace Scripts.StateMachine
{
    public class TurnBasedStateMachine : StateMachineBase
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
            ChangeState<SetupTbState>();
        }
    }
}