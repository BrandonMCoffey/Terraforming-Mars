using UnityEngine;

namespace Scripts.Data
{
    [CreateAssetMenu(menuName = "TM/Icon Data")]
    public class IconData : ScriptableObject
    {
        [Header("Resources")]
        [SerializeField] private Sprite _credits = null;
        [SerializeField] private Sprite _iron = null;
        [SerializeField] private Sprite _titanium = null;
        [SerializeField] private Sprite _plants = null;
        [SerializeField] private Sprite _energy = null;
        [SerializeField] private Sprite _heat = null;
        [Header("Resources (Circular)")]
        [SerializeField] private Sprite _creditsClean = null;
        [SerializeField] private Sprite _ironCircle = null;
        [SerializeField] private Sprite _titaniumCircle = null;
        [SerializeField] private Sprite _plantsCircle = null;
        [SerializeField] private Sprite _energyCircle = null;
        [Header("Special Resources")]
        [SerializeField] private Sprite _bacteria = null;
        [SerializeField] private Sprite _animal = null;
        [Header("Special Resources (Circular)")]
        [SerializeField] private Sprite _bacteriaCircle = null;
        [SerializeField] private Sprite _animalCircle = null;
        [Header("Resource Level")]
        [SerializeField] private Sprite _resourceLevelUp = null;
        [SerializeField] private Sprite _resourceLevelDown = null;
        [Header("Map Hexagons")]
        [SerializeField] private Sprite _ocean = null;
        [SerializeField] private Sprite _forest = null;
        [SerializeField] private Sprite _city = null;
        [SerializeField] private Sprite _nuke = null;
        [Header("Sciences")]
        [SerializeField] private Sprite _atomicScience = null;
        [SerializeField] private Sprite _earth = null;
        [SerializeField] private Sprite _mars = null;
        [SerializeField] private Sprite _cityCircle = null;

        public Sprite GetLevel(bool up = true) => up ? _resourceLevelUp : _resourceLevelDown;

        public Sprite GetResource(ResourceType type, bool isCircular = false)
        {
            return type switch
            {
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
            return type switch
            {
                TileType.Ocean  => _ocean,
                TileType.Forest => _forest,
                TileType.City   => _city,
                TileType.Nuke   => _nuke,
                _               => null
            };
        }

        public Sprite GetScience(ScienceType type)
        {
            return type switch
            {
                ScienceType.Atomic => _atomicScience,
                ScienceType.Earth  => _earth,
                ScienceType.Mars   => _mars,
                ScienceType.City   => _cityCircle,
                _                  => null
            };
        }
    }
}