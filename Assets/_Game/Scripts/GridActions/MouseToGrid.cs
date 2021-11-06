using Input;
using Scripts.Grid;
using UnityEngine;

namespace Scripts.GridActions
{
    public class MouseToGrid : MonoBehaviour
    {
        [SerializeField] private InputController _inputController;

        private Camera _mainCamera;
        private Ray _mouseRay;

        private bool _lockActions;

        private void Awake()
        {
            if (_inputController == null) {
                _inputController = FindObjectOfType<InputController>();
            }
            _mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            _inputController.MouseMoved += OnMouseMoved;
            _inputController.LeftClick += OnLeftClick;
        }

        private void OnDisable()
        {
            _inputController.MouseMoved -= OnMouseMoved;
            _inputController.LeftClick -= OnLeftClick;
        }

        private void Update()
        {
            Physics.Raycast(_mouseRay, out var hit, 100f);
            if (hit.collider == null) {
                HoverSelectedController.instance.DisableHover();
                return;
            }

            GridSlot slot = hit.collider.GetComponent<GridSlot>();
            if (slot != null) {
                slot.OnHover();
            }
        }

        public void LockActions(bool locked)
        {
            if (locked) {
                HoverSelectedController.instance.DisableSelected(true);
            }
            _lockActions = locked;
        }

        private void OnMouseMoved(Vector2 mousePos)
        {
            _mouseRay = _mainCamera.ScreenPointToRay(mousePos);
        }

        private void OnLeftClick(Vector2 mousePos)
        {
            if (_lockActions) return;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            Physics.Raycast(ray, out var hit, 100f);
            if (hit.collider == null) {
                HoverSelectedController.instance.DisableSelected();
                return;
            }

            GridSlot slot = hit.collider.GetComponent<GridSlot>();
            if (slot != null) {
                slot.OnSelect(true);
            }
        }
    }
}