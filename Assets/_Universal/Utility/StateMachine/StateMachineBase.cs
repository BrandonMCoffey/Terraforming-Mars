using System.Collections.Generic;
using UnityEngine;

namespace Utility.StateMachine
{
    public abstract class StateMachineBase : MonoBehaviour
    {
        [SerializeField] private bool _debug;

        private StateBase _currentStateBase;
        private Stack<StateBase> _previousStates = new Stack<StateBase>(4);

        private static int _previousStateMax = 4;

        protected bool InTransition { get; private set; }
        public StateBase CurrentStateBase => _currentStateBase;
        private bool _currentStateIsNull = true;

        private void Update()
        {
            if (_currentStateIsNull || InTransition) return;
            CurrentStateBase.Tick();
        }

        public void ChangeState(StateBase state)
        {
            if (!state.gameObject == gameObject) return;
            InitiateStateChange(state);
        }

        public void ChangeState<T>() where T : StateBase
        {
            T targetState = GetComponent<T>();
            if (targetState == null) {
                Debug.LogWarning("Cannot change to state, as it is not a component on the state machine", gameObject);
                return;
            }
            InitiateStateChange(targetState);
        }

        public void RevertState()
        {
            if (_previousStates.Count <= 0) return;

            StateBase previousStateBase = _previousStates.Pop();
            InitiateStateChange(previousStateBase);
        }

        private void InitiateStateChange(StateBase targetStateBase)
        {
            if (targetStateBase == null || CurrentStateBase == targetStateBase || InTransition) return;
            Transition(targetStateBase);
        }

        private void Transition(StateBase newStateBase)
        {
            InTransition = true;
            if (!_currentStateIsNull) {
                if (_debug) Debug.Log("<color=yellow>Exit State: </color>" + CurrentStateBase.GetType().Name);
                CurrentStateBase.Exit();
            }
            AddPreviousState();
            _currentStateBase = newStateBase;
            if (_debug) Debug.Log("<color=yellow>Enter State: </color>" + CurrentStateBase.GetType().Name);
            CurrentStateBase.Enter();
            _currentStateIsNull = false;
            InTransition = false;
        }

        private void AddPreviousState()
        {
            if (_currentStateIsNull) return;
            if (_previousStates.Count >= _previousStateMax) {
                // TODO: Remove last state
            }
            _previousStates.Push(CurrentStateBase);
        }
    }
}