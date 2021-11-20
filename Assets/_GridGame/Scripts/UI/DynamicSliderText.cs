using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class DynamicSliderText : MonoBehaviour
    {
        [SerializeField] private string _beforeText = "";
        [SerializeField] private Slider _slider = null;
        [SerializeField] private string _afterText = "%";

        private TextMeshProUGUI _text;
        private bool _missingSlider;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void OnValidate()
        {
            _missingSlider = _slider == null;
        }

        private void OnEnable()
        {
            if (_missingSlider) return;
            _slider.onValueChanged.AddListener(SetPercent);
        }

        private void OnDisable()
        {
            if (_missingSlider) return;
            _slider.onValueChanged.RemoveListener(SetPercent);
        }

        private void Start()
        {
            if (_missingSlider) return;
            SetPercent(_slider.value);
        }

        public void SetPercent(float percent)
        {
            int value = Mathf.RoundToInt(percent * 100);
            _text.text = _beforeText + value + _afterText;
        }
    }
}