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
            if (_debug) Debug.Log("User Input: Confirm");
            Confirm?.Invoke();
        }

        public void OnCancel(InputValue value)
        {
            if (_debug) Debug.Log("User Input: Cancel");
            Cancel?.Invoke();
        }

        public void OnSelectLeft(InputValue value)
        {
            if (_debug) Debug.Log("User Input: Select Left");
            SelectLeft?.Invoke();
        }

        public void OnSelectRight(InputValue value)
        {
            if (_debug) Debug.Log("User Input: Select Right");
            SelectRight?.Invoke();
        }
    }
}