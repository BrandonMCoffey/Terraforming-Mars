using System;
using System.Collections.Generic;
using Scripts.Data.Structs;
using Scripts.Enums;
using UnityEngine;
using Utility.Inspector;
using Utility.Other;

namespace Scripts.Data
{
    [CreateAssetMenu(menuName = "TM/Planet Data")]
    public class PlanetData : ScriptableObject
    {
        #region Planet Info

        [Header("Planet Info")]
        [SerializeField] private PlanetType _planetType = PlanetType.Mars;
        [SerializeField] private string _planetName = "Planet";
        [SerializeField] private string _planetSwitchDescription = "(Description)";
        [SerializeField] private Sprite _planetSprite = null;

        public PlanetType PlanetType => _planetType;
        public string PlanetName => _planetName;
        public string PlanetSwitchDescription => _planetSwitchDescription;
        public Sprite PlanetSprite => _planetSprite;

        #endregion

        #region Planet Resources

        [Header("Planet Resources")]
        [SerializeField] private List<ResourceType> _availableResources = new List<ResourceType>();

        public List<ResourceType> AvailableResources => _availableResources;

        #endregion

        #region Planet Goals

        [Header("Oxygen")]
        [SerializeField] private bool _requireOxygen = true;
        [SerializeField] private PlanetStatusLevel _oxygenLevel = new PlanetStatusLevel(0, 14, 1);
        [SerializeField] [ReadOnly] private int _oxygenStatus;
        [SerializeField] [ReadOnly] private bool _oxygenComplete;

        [Header("Water")]
        [SerializeField] private bool _requireWater = true;
        [SerializeField] private PlanetStatusLevel _waterLevel = new PlanetStatusLevel(0, 12, 1);
        [SerializeField] [ReadOnly] private int _waterStatus;
        [SerializeField] [ReadOnly] private bool _waterComplete;

        [Header("Heat")]
        [SerializeField] private bool _requireHeat = true;
        [SerializeField] private PlanetStatusLevel _heatLevel = new PlanetStatusLevel(-30, 8, 2);
        [SerializeField] [ReadOnly] private int _heatStatus = -30;
        [SerializeField] [ReadOnly] private bool _heatComplete;

        [Header("Magnetic Field")]
        [SerializeField] private bool _requireMagneticField = true;
        [SerializeField] [ReadOnly] private bool _magneticFieldComplete;

        public bool RequireOxygen => _requireOxygen;
        public bool RequireWater => _requireWater;
        public bool RequireHeat => _requireHeat;
        public PlanetStatusLevel HeatLevel => _heatLevel;
        public bool RequireMagneticField => _requireMagneticField;

        public event Action OnPlanetStatusChanged;

        public event Action OnPlanetTerraformed;

        #endregion

        public void Setup()
        {
            _oxygenStatus = _oxygenLevel.MinValue;
            _heatStatus = _heatLevel.MinValue;
            _waterStatus = _waterLevel.MinValue;
            _oxygenComplete = false;
            _heatComplete = false;
            _waterComplete = false;
            _magneticFieldComplete = false;
            OnPlanetStatusChanged?.Invoke();
        }

        // ------------------------------------------

        #region Planet Goals

        public void IncreaseStatus(PlanetStatusType type)
        {
            switch (type) {
                case PlanetStatusType.Oxygen:
                    if (!_requireOxygen || _oxygenComplete) return;
                    _oxygenStatus += _oxygenLevel.StepValue;
                    if (_oxygenStatus >= _oxygenLevel.MaxValue) {
                        _oxygenStatus = _oxygenLevel.MaxValue;
                        _oxygenComplete = true;
                    }
                    break;
                case PlanetStatusType.Heat:
                    if (!_requireHeat || _heatComplete) return;
                    _heatStatus += _heatLevel.StepValue;
                    if (_heatStatus >= _heatLevel.MaxValue) {
                        _heatStatus = _heatLevel.MaxValue;
                        _heatComplete = true;
                    }
                    break;
                case PlanetStatusType.Water:
                    if (!_requireWater || _waterComplete) return;
                    _waterStatus += _waterLevel.StepValue;
                    if (_waterStatus >= _waterLevel.MaxValue) {
                        _waterStatus = _waterLevel.MaxValue;
                        _waterComplete = true;
                    }
                    break;
                case PlanetStatusType.MagneticField:
                    if (!_requireMagneticField || _magneticFieldComplete) return;
                    _magneticFieldComplete = true;
                    break;
            }
            OnPlanetStatusChanged?.Invoke();
            CheckPlanetComplete();
        }

        private void CheckPlanetComplete()
        {
            if (_requireOxygen && !_oxygenComplete) return;
            if (_requireHeat && !_heatComplete) return;
            if (_requireWater && !_waterComplete) return;
            if (_requireMagneticField && !_magneticFieldComplete) return;
            OnPlanetTerraformed?.Invoke();
        }

        public int GetLevel(PlanetStatusType type)
        {
            return type switch
            {
                PlanetStatusType.Oxygen        => _oxygenStatus,
                PlanetStatusType.Heat          => _heatStatus,
                PlanetStatusType.Water         => _waterStatus,
                PlanetStatusType.MagneticField => _magneticFieldComplete ? 1 : 0,
                _                              => 0
            };
        }

        public float GetLevel0To1(PlanetStatusType type)
        {
            return type switch
            {
                PlanetStatusType.Oxygen        => CustomMath.Map(_oxygenStatus, _oxygenLevel.MinValue, _oxygenLevel.MaxValue, 0, 1),
                PlanetStatusType.Heat          => CustomMath.Map(_heatStatus, _heatLevel.MinValue, _heatLevel.MaxValue, 0, 1),
                PlanetStatusType.Water         => CustomMath.Map(_waterStatus, _waterLevel.MinValue, _waterLevel.MaxValue, 0, 1),
                PlanetStatusType.MagneticField => _magneticFieldComplete ? 1 : 0,
                _                              => 0
            };
        }

        #endregion
    }
}