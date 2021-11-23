using System;
using UnityEngine;

namespace Scripts.Data
{
    [CreateAssetMenu(menuName = "TM/Planet Data")]
    public class PlanetData : ScriptableObject
    {
        [SerializeField] [Range(0, 1)] private float _oxygenLevel = 0;
        [SerializeField] [Range(0, 1)] private float _heatLevel = 0;
        [SerializeField] [Range(0, 1)] private float _waterLevel = 0;

        public event Action<TerraformingValues> OnValuesChanges;

        private TerraformingValues _state;

        private void OnValidate()
        {
            CheckForUpdates();
        }

        public bool CheckForUpdates(bool force = false)
        {
            bool changed = false;
            if (Math.Abs(_state.Oxygen - _oxygenLevel) > 0.001f) {
                changed = true;
                _state.Oxygen = _oxygenLevel;
            }
            if (Math.Abs(_state.Heat - _heatLevel) > 0.001f) {
                changed = true;
                _state.Heat = _heatLevel;
            }
            if (Math.Abs(_state.Water - _waterLevel) > 0.001f) {
                changed = true;
                _state.Water = _waterLevel;
            }
            if (changed || force) {
                OnValuesChanges?.Invoke(_state);
            }
            return changed;
        }
    }

    public struct TerraformingValues
    {
        public float Oxygen;
        public float Heat;
        public float Water;
    }
}