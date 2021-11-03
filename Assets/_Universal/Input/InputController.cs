using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private bool _debug = false;

        public event Action Confirm = delegate { };
        public event Action Cancel = delegate { };
        public event Action SelectLeft = delegate { };
        public event Action SelectRight = delegate { };
        public event Action<Vector2> MouseMoved = delegate { };
        public event Action<Vector2> LeftClick = delegate { };
        public event Action<Vector2> RightClick = delegate { };
        public event Action UILeftClick = delegate { };
        public event Action UIRightClick = delegate { };

        private Vector2 _mousePosition;

        private void Update()
        {
            Vector2 newMousePosition = Mouse.current.position.ReadValue();
            if (_mousePosition != newMousePosition) {
                _mousePosition = newMousePosition;
                MouseMoved.Invoke(_mousePosition);
            }
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

        public void OnLeftClick(InputValue value)
        {
            DebugInput("Left Click");
            if (IsMouseOverUI()) {
                UILeftClick?.Invoke();
            } else {
                LeftClick?.Invoke(_mousePosition);
            }
        }

        public void OnRightClick(InputValue value)
        {
            DebugInput("Right Click");
            if (IsMouseOverUI()) {
                UIRightClick?.Invoke();
            } else {
                RightClick?.Invoke(_mousePosition);
            }
        }

        private void DebugInput(string inputAction)
        {
            if (_debug) {
                Debug.Log("<color=aqua>User Input: </color>" + inputAction);
            }
        }

        public static bool IsMouseOverUI()
        {
            return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
        }
    }
}