using Input;
using UnityEngine;

namespace Scripts
{
    public class MouseToGrid : MonoBehaviour
    {
        [SerializeField] private InputController _inputController;

        private void Awake()
        {
            if (_inputController == null) {
                _inputController = FindObjectOfType<InputController>();
            }
        }

        private void OnEnable()
        {
            _inputController.LeftClick += OnLeftClick;
        }

        private void OnDisable()
        {
            _inputController.LeftClick -= OnLeftClick;
        }

        private static void OnLeftClick(Vector2 mousePos)
        {
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            Physics.Raycast(ray, out var hit, 100f);
            if (hit.collider == null) return;

            GridSlot slot = hit.collider.GetComponent<GridSlot>();
            if (slot != null) {
                slot.OnSelect();
            }
        }
    }
}