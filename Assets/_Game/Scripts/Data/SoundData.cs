using UnityEngine;
using Utility.Audio.Systems.Events;

namespace Scripts.Data
{
    [CreateAssetMenu(menuName = "TM/Sound Data")]
    public class SoundData : ScriptableObject
    {
        [SerializeField] private SfxEvent _asteroidSfx;
        [SerializeField] private SfxEvent _powerPlantSfx;
        [SerializeField] private SfxEvent _aquiferSfx;
        [SerializeField] private SfxEvent _citySfx;
        [SerializeField] private SfxEvent _forestSfx;
        [SerializeField] private SfxEvent _placeTileSfx;
        [SerializeField] private SfxEvent _buttonPressSfx;

        public SfxEvent AsteroidSfx => _asteroidSfx;
        public SfxEvent PowerPlantSfx => _powerPlantSfx;
        public SfxEvent AquiferSfx => _aquiferSfx;
        public SfxEvent CitySfx => _citySfx;
        public SfxEvent ForestSfx => _forestSfx;
        public SfxEvent PlaceTileSfx => _placeTileSfx;
        public SfxEvent ButtonPressSfx => _buttonPressSfx;
    }
}