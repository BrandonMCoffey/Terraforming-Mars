using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.UI.ModalWindows
{
    public class DraggableUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler
    {
        private RectTransform _rect;
        private Vector2 _prevMousePos;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            transform.SetAsLastSibling();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _prevMousePos = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 mousePos = eventData.position;
            Vector2 diff = mousePos - _prevMousePos;

            var newPos = _rect.position;
            Vector3 oldPos = newPos;
            newPos += new Vector3(diff.x, diff.y, transform.position.z);
            _rect.position = newPos;
            if (IsRectInScreen(_rect)) {
                _prevMousePos = mousePos;
            } else {
                _rect.position = oldPos;
            }
        }

        private static bool IsRectInScreen(RectTransform rect)
        {
            var corners = new Vector3[4];
            rect.GetWorldCorners(corners);
            Rect screen = new Rect(0, 0, Screen.width, Screen.height);
            return corners.Count(corner => screen.Contains(corner)) == 4;
        }
    }
}