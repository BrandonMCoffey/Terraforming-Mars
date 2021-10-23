using UnityEngine;

namespace Scripts.StateMachine
{
    public abstract class State : MonoBehaviour
    {
        public abstract void Enter();
        public abstract void Tick();

        public virtual void Exit()
        {
        }
    }
}