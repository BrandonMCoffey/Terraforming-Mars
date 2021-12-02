using System;
using Scripts.Enums;
using UnityEngine;

namespace Scripts.Data
{
    [CreateAssetMenu(menuName = "TM/Icon Data")]
    public class IconData : ScriptableObject
    {
        [Header("Resources")]
        [SerializeField] private Sprite _credits;
        [SerializeField] private Sprite _iron;
        [SerializeField] private Sprite _titanium;
        [SerializeField] private Sprite _plants;
        [SerializeField] private Sprite _energy;
        [SerializeField] private Sprite _heat;
        [Header("Resources (Circular)")]
        [SerializeField] private Sprite _creditsClean;
        [SerializeField] private Sprite _ironCircle;
        [SerializeField] private Sprite _titaniumCircle;
        [SerializeField] private Sprite _plantsCircle;
        [SerializeField] private Sprite _energyCircle;
        [Header("Special Resources")]
        [SerializeField] private Sprite _bacteria;
        [SerializeField] private Sprite _animal;
        [Header("Special Resources (Circular)")]
        [SerializeField] private Sprite _bacteriaCircle;
        [SerializeField] private Sprite _animalCircle;
        [Header("Resource Level")]
        [SerializeField] private Sprite _resourceLevelUp;
        [Header("Map Hexagons")]
        [SerializeField] private Sprite _ocean;
        [SerializeField] private Sprite _forest;
        [SerializeField] private Sprite _city;
        [SerializeField] private Sprite _nuke;
        [Header("Sciences")]
        [SerializeField] private Sprite _atomicScience;
        [SerializeField] private Sprite _earth;
        [SerializeField] private Sprite _mars;
        [SerializeField] private Sprite _cityCircle;
        [Header("Other")]
        [SerializeField] private Sprite _spacer;

        public Sprite ResourceLevelUp => _resourceLevelUp;
        public Sprite Spacer => _spacer;

        public Sprite GetResource(ResourceType type, bool isCircular = false)
        {
            return type switch {
                ResourceType.Credits  => isCircular ? _creditsClean : _credits,
                ResourceType.Iron     => isCircular ? _ironCircle : _iron,
                ResourceType.Titanium => isCircular ? _titaniumCircle : _titanium,
                ResourceType.Plant    => isCircular ? _plantsCircle : _plants,
                ResourceType.Energy   => isCircular ? _energyCircle : _energy,
                ResourceType.Heat     => _heat,
                ResourceType.Bacteria => isCircular ? _bacteriaCircle : _bacteria,
                ResourceType.Animal   => isCircular ? _animalCircle : _animal,
                _                     => null
            };
        }

        public Sprite GetTile(TileType type)
        {
            return type switch {
                TileType.Ocean  => _ocean,
                TileType.Forest => _forest,
                TileType.City   => _city,
                TileType.Nuke   => _nuke,
                _               => null
            };
        }

        public Sprite GetScience(ScienceType type)
        {
            return type switch {
                ScienceType.Atomic => _atomicScience,
                ScienceType.Earth  => _earth,
                ScienceType.Mars   => _mars,
                ScienceType.City   => _cityCircle,
                _                  => null
            };
        }

        public Sprite GetIcon(PatentResourceType type)
        {
            return type switch {
                PatentResourceType.Earth    => _earth,
                PatentResourceType.Mars     => _mars,
                PatentResourceType.Science  => _atomicScience,
                PatentResourceType.Iron     => _ironCircle,
                PatentResourceType.Titanium => _titaniumCircle,
                PatentResourceType.Plants   => _plantsCircle,
                PatentResourceType.Energy   => _energyCircle,
                PatentResourceType.Heat     => _heat,
                PatentResourceType.City     => _cityCircle,
                PatentResourceType.Bacteria => _bacteriaCircle,
                PatentResourceType.Animal   => _animalCircle,
                _                           => null
            };
        }
    }
}