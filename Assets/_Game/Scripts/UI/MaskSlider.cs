using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class MaskSlider : MonoBehaviour
    {
        [Header("Slides from bottom to top")]
        [SerializeField] private Image _image = null;
        [SerializeField] private RectMask2D _mask = null;
        [SerializeField] private Slider _slider = null;
        [SerializeField] private float _offsetFromBottom = 0;

        private float _height;

        private void Start()
        {
            _height = _image.sprite.rect.height - _offsetFromBottom;
            if (_height < 0) _height = 0;
            OnSliderChange(_slider.value);
        }

        public void OnSliderChange(float value)
        {
            value = Mathf.Clamp01(value);
            var padding = _mask.padding;
            padding.w = _height * (1 - value);
            _mask.padding = padding;
        }
    }
}