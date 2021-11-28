using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.Grid
{
    [RequireComponent(typeof(Image))]
    public class HexTileClickable : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private float _alphaThreshold = 0.1f;

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

        public void SetColor(Color color)
        {
            _image.color = color;
        }

        public event Action OnClicked;
    }
}