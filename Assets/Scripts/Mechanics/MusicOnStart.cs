using System.Collections;
using UnityEngine;
using Utility.Audio.Systems.Controllers;
using Utility.Audio.Systems.Events;

namespace Scripts.Mechanics
{
    public class MusicOnStart : MonoBehaviour
    {
        [SerializeField] private AudioSourceController _source1;
        [SerializeField] private AudioSourceController _source2;
        [SerializeField] private SfxVariant _music;
        [SerializeField] private float _length;
        [SerializeField] private float _overlap;

        private float _play1;
        private float _play2;

        private void Start()
        {
            _play1 = Time.time + _length * 2 - _overlap;
            _play2 = Time.time + _length - _overlap;
            _music.Play(_source1);
        }

        private void Update()
        {
            float time = Time.time;
            if (time > _play1 && !_source1.IsPlaying) {
                _music.Play(_source1);
                StartCoroutine(FadeIn(_source1, _overlap * 0.8f));
                _play1 = Time.time + _length * 2 - _overlap;
            }
            if (time > _play2 && !_source2.IsPlaying) {
                _music.Play(_source2);
                StartCoroutine(FadeIn(_source2, _overlap * 0.8f));
                _play2 = Time.time + _length * 2 - _overlap;
            }
        }

        private static IEnumerator FadeIn(AudioSourceController source, float duration)
        {
            for (float t = 0; t < duration; t += Time.deltaTime) {
                source.SetCustomVolume(t / duration);
                yield return null;
            }
            source.SetCustomVolume(1);
        }
    }
}