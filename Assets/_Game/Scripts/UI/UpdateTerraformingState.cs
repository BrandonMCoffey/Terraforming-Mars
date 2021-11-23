using Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class UpdateTerraformingState : MonoBehaviour
    {
        [SerializeField] private PlanetData _planetData = null;
        [SerializeField] private Slider _oxygenSlider = null;
        [SerializeField] private Slider _heatSlider = null;
        [SerializeField] private Slider _waterSlider = null;

        private void OnEnable()
        {
            _planetData.OnValuesChanges += UpdateValues;
        }

        private void OnDisable()
        {
            _planetData.OnValuesChanges -= UpdateValues;
        }

        private void Start()
        {
            _planetData.CheckForUpdates(true);
        }

        private void UpdateValues(TerraformingValues values)
        {
            if (_oxygenSlider != null) _oxygenSlider.value = values.Oxygen;
            if (_heatSlider != null) _heatSlider.value = values.Heat;
            if (_waterSlider != null) _waterSlider.value = values.Water;
        }
    }
}