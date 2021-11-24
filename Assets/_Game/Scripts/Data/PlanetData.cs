using System;
using UnityEngine;
using Utility.Other;

namespace Scripts.Data
{
    [CreateAssetMenu(menuName = "TM/Planet Data")]
    public class PlanetData : ScriptableObject
    {
        public const int MaxOxygen = 14;
        public const int MinHeat = -30;
        public const int MaxHeat = 8;
        public const int MaxOcean = 12;

        [SerializeField] [Range(0, MaxOxygen)] private int _oxygenLevel = 0;
        [SerializeField] [Range(MinHeat, MaxHeat)]
        private int _heatLevel = -30;
        [SerializeField] [Range(0, MaxOcean)] private int _waterLevel = 0;

        public event Action OnValuesChanges;

        private int _oxygen;
        private int _heat = -30;
        private int _water;

#if UNITY_EDITOR
        private void OnValidate()
        {
            CheckForUpdates();
        }
#endif

        public int GetLevel(PlanetStatusType type, bool normalize = false)
        {
            return type switch
            {
                PlanetStatusType.Oxygen => _oxygen,
                PlanetStatusType.Heat   => _heat,
                PlanetStatusType.Water  => _water,
                _                       => 0
            };
        }

        public float GetLevel0To1(PlanetStatusType type)
        {
            return type switch
            {
                PlanetStatusType.Oxygen => CustomMath.Map(_oxygen, 0, MaxOxygen, 0, 1),
                PlanetStatusType.Heat   => CustomMath.Map(_heat, MinHeat, MaxHeat, 0, 1),
                PlanetStatusType.Water  => CustomMath.Map(_water, 0, MaxOcean, 0, 1),
                _                       => 0
            };
        }

        public bool CheckForUpdates(bool force = false)
        {
            bool changed = false;
            if (Math.Abs(_oxygen - _oxygenLevel) > 0.001f) {
                changed = true;
                _oxygen = _oxygenLevel;
            }
            if (Math.Abs(_heat - _heatLevel) > 0.001f) {
                changed = true;
                _heat = _heatLevel;
            }
            if (Math.Abs(_water - _waterLevel) > 0.001f) {
                changed = true;
                _water = _waterLevel;
            }
            if (changed || force) {
                OnValuesChanges?.Invoke();
            }
            return changed;
        }
    }
}