using System.Collections.Generic;
using UnityEngine;

namespace Scripts.StateMachine.Base
{
    public abstract class StateMachineBase : MonoBehaviour
    {
        private State _currentState;
        private Stack<State> _previousStates = new Stack<State>(4);

        private static int _previousStateMax = 4;

        protected bool InTransition { get; private set; }
        public State CurrentState => _currentState;

        private void Update()
        {
            if (CurrentState == null || InTransition) return;
            CurrentState.Tick();
        }

        public void ChangeState<T>() where T : State
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

            State previousState = _previousStates.Pop();
            InitiateStateChange(previousState);
        }

        private void InitiateStateChange(State targetState)
        {
            if (CurrentState == targetState || InTransition) return;
            Transition(targetState);
        }

        private void Transition(State newState)
        {
            InTransition = true;
            CurrentState?.Exit();
            AddPreviousState();
            _currentState = newState;
            CurrentState?.Enter();
            InTransition = false;
        }

        private void AddPreviousState()
        {
            if (CurrentState == null) return;
            if (_previousStates.Count >= _previousStateMax) {
                // TODO: Remove last state
            }
            _previousStates.Push(CurrentState);
        }
    }
}