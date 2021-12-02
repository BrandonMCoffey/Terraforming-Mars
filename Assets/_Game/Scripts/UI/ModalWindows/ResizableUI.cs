using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.UI.ModalWindows
{
    public class ResizableUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler
    {
        [SerializeField] private RectTransform _transformToResize;
        [SerializeField] private Vector2Int _minMaxWidth;
        [SerializeField] private Vector2Int _minMaxHeight;

        private RectTransform _rect;
        private Vector2 _startPos;
        private Vector2 _startMousePos;

        private Vector2 _size;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            var rect = _transformToResize.rect;
            _size = new Vector2(rect.width, rect.height);
            transform.SetAsLastSibling();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startPos = _transformToResize.position;
            _startMousePos = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var mousePos = eventData.position;
            var sizeOffset = mousePos - _startMousePos;

            sizeOffset.x = Mathf.Clamp(sizeOffset.x, _minMaxWidth.x - _size.x, _minMaxWidth.y - _size.x);
            sizeOffset.y = -sizeOffset.y;
            sizeOffset.y = Mathf.Clamp(sizeOffset.y, _minMaxHeight.x - _size.y, _minMaxHeight.y - _size.y);

            var size = _size + new Vector2(sizeOffset.x, sizeOffset.y);
            _transformToResize.sizeDelta = size;
            sizeOffset.y = -sizeOffset.y;
            var pos = _startPos + sizeOffset * 0.5f;
            _transformToResize.position = pos;
        }
    }
}