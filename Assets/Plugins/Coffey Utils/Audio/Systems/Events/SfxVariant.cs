﻿using UnityEngine;
using UnityEngine.Audio;
using Utility.Audio.Systems.Base;

namespace Utility.Audio.Systems.Events
{
    [CreateAssetMenu(menuName = "Utility/Sound System/Sfx Variant")]
    public class SfxVariant : SfxBase
    {
        [Header("Audio Clip Settings")]
        [SerializeField] private SfxReference _clip;
        [SerializeField] private bool _loop;

        [Header("Volume Settings")]
        [SerializeField] [Range(0f, 1f)] private float _volume = 1f;
        [SerializeField] [Range(.25f, 3f)] private float _pitch = 1f;

        [Header("Spatial Settings")]
        [SerializeField] [Range(-1f, 1f)] private float _stereoPan;
        [SerializeField] [Range(0f, 1f)] private float _spatialBlend;
        [SerializeField] [Range(0f, 1.1f)] private float _reverbZoneMix = 1f;

        [Header("Mixer Settings")]
        [SerializeField] private AudioMixerGroup _mixerGroup;

        public override SfxProperties GetSourceProperties()
        {
            // Ensure clip is not null or the same (prevent recursion)
            if (_clip.NullTest() || _clip.TestSame(this)) return new SfxProperties(true);

            // Find Reference Source Properties
            var referenceProperties = _clip.GetSourceProperties();

            // Create Current Source Properties
            var myProperties = new SfxProperties(_loop, _volume, _pitch, _stereoPan, _spatialBlend, _reverbZoneMix, _mixerGroup);

            // Add properties together and return
            return myProperties.AddProperties(referenceProperties);
        }
    }
}