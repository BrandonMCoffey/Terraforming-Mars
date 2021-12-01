using Scripts.Data;
using Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Displays
{
    public class DisplayPlanetStatus : MonoBehaviour
    {
        [SerializeField] private PlanetData _planetData;
        [SerializeField] private PlanetStatusType _statusType = PlanetStatusType.Oxygen;
        [SerializeField] private Slider _slider;

        private void OnEnable()
        {
            _planetData.OnPlanetStatusChanged += UpdateValue;
            UpdateValue();
        }

        private void OnDisable()
        {
            _planetData.OnPlanetStatusChanged -= UpdateValue;
        }

        private void UpdateValue()
        {
            if (_planetData == null || _slider == null) return;
            _slider.value = _planetData.GetLevel0To1(_statusType);
            _slider.onValueChanged?.Invoke(_slider.value);
        }
    }
}