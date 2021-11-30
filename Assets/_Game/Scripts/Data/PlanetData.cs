using System;
using System.Collections.Generic;
using GridTool.DataScripts;
using Scripts.Enums;
using UnityEngine;
using Utility.CustomFloats;
using Utility.Other;

namespace Scripts.Data
{
    [CreateAssetMenu(menuName = "TM/Planet Data")]
    public class PlanetData : ScriptableObject
    {
        #region Planet Info

        [Header("Planet Info")]
        [SerializeField] private string _planetName = "Planet";
        [SerializeField] private string _planetSwitchDescription = "(Description)";
        [SerializeField] private Sprite _planetSprite = null;

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
        [SerializeField] private MinMaxInt _oxygenLevel = new MinMaxInt(0, 14);
        [SerializeField] private int _oxygenStep = 1;
        [SerializeField] [ReadOnly] private int _oxygenStatus = 0;

        [Header("Water")]
        [SerializeField] private bool _requireWater = true;
        [SerializeField] private MinMaxInt _waterLevel = new MinMaxInt(0, 12);
        [SerializeField] private int _waterStep = 1;
        [SerializeField] [ReadOnly] private int _waterStatus = 0;

        [Header("Heat")]
        [SerializeField] private bool _requireHeat = true;
        [SerializeField] private MinMaxInt _minMaxHeat = new MinMaxInt(-30, 8);
        [SerializeField] private int _heatStep = 2;
        [SerializeField] [ReadOnly] private int _heatStatus = -30;

        [Header("Magnetic Field")]
        [SerializeField] private bool _requireMagneticField = true;
        [SerializeField] [ReadOnly] private bool _magneticFieldStatus = false;

        public bool RequireOxygen => _requireOxygen;
        public bool RequireWater => _requireWater;
        public bool RequireHeat => _requireHeat;
        public bool RequireMagneticField => _requireMagneticField;

        #endregion

        // ------------------------------------------

        #region Planet Goals

        public int GetLevel(PlanetStatusType type)
        {
            return type switch
            {
                PlanetStatusType.Oxygen        => _oxygenStatus,
                PlanetStatusType.Heat          => _heatStatus,
                PlanetStatusType.Water         => _waterStatus,
                PlanetStatusType.MagneticField => _magneticFieldStatus ? 1 : 0,
                _                              => 0
            };
        }

        public float GetLevel0To1(PlanetStatusType type)
        {
            return type switch
            {
                PlanetStatusType.Oxygen        => CustomMath.Map(_oxygenStatus, _oxygenLevel.MinValue, _oxygenLevel.MaxValue, 0, 1),
                PlanetStatusType.Heat          => CustomMath.Map(_heatStatus, _minMaxHeat.MinValue, _minMaxHeat.MaxValue, 0, 1),
                PlanetStatusType.Water         => CustomMath.Map(_waterStatus, _waterLevel.MinValue, _waterLevel.MaxValue, 0, 1),
                PlanetStatusType.MagneticField => _magneticFieldStatus ? 1 : 0,
                _                              => 0
            };
        }

        #endregion
    }
}