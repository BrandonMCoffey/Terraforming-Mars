using Scripts.Data;
using Scripts.Enums;
using TMPro;
using UnityEngine;

namespace Scripts.UI.Displays
{
    public class PlanetStatusDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private PlanetStatusType _statusType;
        [SerializeField] private PlanetData _planet;

        private void OnEnable()
        {
            _planet.OnPlanetStatusChanged += UpdateText;
            UpdateText();
        }

        private void OnDisable()
        {
            _planet.OnPlanetStatusChanged -= UpdateText;
        }

        private void UpdateText()
        {
            switch (_statusType) {
                case PlanetStatusType.Oxygen:
                    _text.text = _planet.GetLevel(PlanetStatusType.Oxygen) + "%";
                    break;
                case PlanetStatusType.Heat:
                    _text.text = _planet.GetLevel(PlanetStatusType.Heat) + "*";
                    break;
                case PlanetStatusType.Water:
                    _text.text = _planet.GetLevel(PlanetStatusType.Water) + " / " + _planet.WaterLevel.MaxValue;
                    break;
                case PlanetStatusType.MagneticField:
                    _text.text = _planet.MagneticFieldComplete ? "Active" : "Inactive";
                    break;
            }
        }
    }
}