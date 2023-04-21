using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.Grid
{
    [RequireComponent(typeof(Image))]
    public class HexTileClickable : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private float _alphaThreshold = 0.1f;

        public event Action OnClicked;
        public event Action OnEnterHover;
        public event Action OnExitHover;

        private Image _image;

        private void Start()
        {
            _image = GetComponent<Image>();
            _image.alphaHitTestMinimumThreshold = _alphaThreshold;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnClicked?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnEnterHover?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnExitHover?.Invoke();
        }
    }
}