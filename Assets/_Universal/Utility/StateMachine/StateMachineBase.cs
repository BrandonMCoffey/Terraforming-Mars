using System.Collections.Generic;
using UnityEngine;

namespace Utility.StateMachine
{
    public abstract class StateMachineBase : MonoBehaviour
    {
        [SerializeField] private bool _debug = false;

        private StateBase _currentStateBase;
        private Stack<StateBase> _previousStates = new Stack<StateBase>(4);

        private static int _previousStateMax = 4;

        protected bool InTransition { get; private set; }
        public StateBase CurrentStateBase => _currentStateBase;

        private void Update()
        {
            if (CurrentStateBase == null || InTransition) return;
            CurrentStateBase.Tick();
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
            if (CurrentStateBase == targetStateBase || InTransition) return;
            Transition(targetStateBase);
        }

        private void Transition(StateBase newStateBase)
        {
            InTransition = true;
            if (_debug) Debug.Log("<color=yellow>Exit StateBase: </color>" + CurrentStateBase?.GetType().Name);
            CurrentStateBase?.Exit();
            AddPreviousState();
            _currentStateBase = newStateBase;
            if (_debug) Debug.Log("<color=yellow>Enter StateBase: </color>" + CurrentStateBase?.GetType().Name);
            CurrentStateBase?.Enter();
            InTransition = false;
        }

        private void AddPreviousState()
        {
            if (CurrentStateBase == null) return;
            if (_previousStates.Count >= _previousStateMax) {
                // TODO: Remove last state
            }
            _previousStates.Push(CurrentStateBase);
        }
    }
}