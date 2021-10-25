using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private bool _debug = false;

        public event Action Confirm = delegate { };
        public event Action Cancel = delegate { };
        public event Action SelectLeft = delegate { };
        public event Action SelectRight = delegate { };

        private void Update()
        {
        }

        public void OnConfirm(InputValue value)
        {
            DebugInput("Confirm");
            Confirm?.Invoke();
        }

        public void OnCancel(InputValue value)
        {
            DebugInput("Cancel");
            Cancel?.Invoke();
        }

        public void OnSelectLeft(InputValue value)
        {
            DebugInput("Select Left");
            SelectLeft?.Invoke();
        }

        public void OnSelectRight(InputValue value)
        {
            DebugInput("Select Right");
            SelectRight?.Invoke();
        }

        private void DebugInput(string inputAction)
        {
            if (_debug) {
                Debug.Log("<color=aqua>User Input: </color>" + inputAction);
            }
        }
    }
}