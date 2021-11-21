using Input;
using UnityEngine;
using Utility.StateMachine;

namespace Scripts.States
{
    public class MasterStateMachine : StateMachineBase
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
            ChangeState<SetupState>();
        }
    }
}