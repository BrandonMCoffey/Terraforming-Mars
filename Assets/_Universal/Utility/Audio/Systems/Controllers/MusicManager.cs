using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using Utility.Audio.Systems.Base;

namespace Utility.Audio.Systems.Controllers
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] [Range(0, 1)] private float _currentMusicVolume = 1;
        [SerializeField] private AudioMixerGroup _musicGroup = null;
        [SerializeField] private AudioSourceController _musicSource1;
        [SerializeField] private AudioSourceController _musicSource2;

        private bool _musicSource1Playing;
        private Coroutine _currentRoutine;

        public float CurrentMusicVolume
        {
            get => _currentMusicVolume;
            private set => Mathf.Clamp(value, 0, 1);
        }

        private AudioSourceController ActiveSource => _musicSource1Playing ? _musicSource1 : _musicSource2;
        private AudioSourceController OtherSource => _musicSource1Playing ? _musicSource2 : _musicSource1;

        private void Start()
        {
            ClaimSources();
        }

        #region Public

        public void BuildMusicPlayers(string playerName)
        {
            if (_musicSource1 == null) {
                GameObject newSource = new GameObject(playerName, typeof(AudioSource));
                newSource.transform.SetParent(transform);
                _musicSource1 = newSource.AddComponent<AudioSourceController>();
            }
            if (_musicSource2 == null) {
                GameObject newSource = new GameObject(playerName, typeof(AudioSource));
                newSource.transform.SetParent(transform);
                _musicSource2 = newSource.AddComponent<AudioSourceController>();
            }
            ClaimSources();
        }

        public void SetMusicVolume(float targetVolume)
        {
            CurrentMusicVolume = targetVolume;
            AudioSourceController activeSource = ActiveSource;
            activeSource.SetCustomVolume(CurrentMusicVolume);
        }

        public void SetMusicVolume(AudioSourceController source, float targetVolume)
        {
            CurrentMusicVolume = targetVolume;
            source.SetCustomVolume(CurrentMusicVolume);
        }

        public void SetMusicVolumeWithBlend(float targetVolume, float volumeBlendDuration)
        {
            AudioSourceController activeSource = ActiveSource;
            StopRoutine();
            _currentRoutine = StartCoroutine(UpdateMusicVolume(activeSource, targetVolume, volumeBlendDuration));
        }

        public void PlayMusic(SfxReference musicClip)
        {
            AudioSourceController activeSource = ActiveSource;

            activeSource.SetSourceProperties(musicClip.GetSourceProperties());
            activeSource.SetCustomVolume(CurrentMusicVolume);
            activeSource.Play();
        }

        public void PlayMusicWithFade(SfxReference musicClip, float transitionDuration)
        {
            AudioSourceController activeSource = ActiveSource;
            StopRoutine();
            _currentRoutine = StartCoroutine(UpdateMusicWithFade(activeSource, musicClip, transitionDuration));
        }

        public void PlayMusicWithCrossFade(SfxReference musicClip, float transitionTime)
        {
            AudioSourceController activeSource = ActiveSource;
            AudioSourceController otherSource = OtherSource;

            _musicSource1Playing = !_musicSource1Playing;

            otherSource.SetSourceProperties(musicClip.GetSourceProperties());
            otherSource.SetCustomVolume(0);
            otherSource.Play();

            StopRoutine();
            _currentRoutine = StartCoroutine(UpdateMusicWithCrossFade(activeSource, otherSource, transitionTime));
        }

        #endregion

        #region Private

        private void ClaimSources()
        {
            _musicSource1.Claimed = false;
            _musicSource1.SetMixer(_musicGroup);
            _musicSource1.Pause();
            _musicSource2.Claimed = false;
            _musicSource2.SetMixer(_musicGroup);
            _musicSource2.Pause();
        }

        private void StopRoutine()
        {
            if (_currentRoutine == null) return;
            StopCoroutine(_currentRoutine);
            var otherSource = OtherSource;
            if (otherSource.IsPlaying) {
                otherSource.Stop();
            }
        }

        private IEnumerator UpdateMusicVolume(AudioSourceController source, float targetVolume, float fadeTime)
        {
            float startingVolume = CurrentMusicVolume;
            for (float t = 0; t <= fadeTime; t += Time.deltaTime) {
                float delta = t / fadeTime;
                float currentVolume = Mathf.Lerp(startingVolume, targetVolume, delta);
                SetMusicVolume(source, currentVolume);
                yield return null;
            }
            SetMusicVolume(source, targetVolume);
        }

        private IEnumerator UpdateMusicWithFade(AudioSourceController activeSource, SfxReference musicClip, float transitionDuration)
        {
            float targetVolume = CurrentMusicVolume;
            if (activeSource.IsPlaying) {
                transitionDuration /= 2;
                float startingVolume = CurrentMusicVolume;
                for (float t = 0; t <= transitionDuration; t += Time.deltaTime) {
                    float delta = t / transitionDuration;
                    float currentVolume = Mathf.Lerp(startingVolume, 0, delta);
                    SetMusicVolume(activeSource, currentVolume);
                    yield return null;
                }
                activeSource.Stop();
            }
            activeSource.SetSourceProperties(musicClip.GetSourceProperties());
            activeSource.SetCustomVolume(0);
            activeSource.Play();

            for (float t = 0; t <= transitionDuration; t += Time.deltaTime) {
                float delta = t / transitionDuration;
                float currentVolume = Mathf.Lerp(0, targetVolume, delta);
                SetMusicVolume(activeSource, currentVolume);
                yield return null;
            }
            SetMusicVolume(activeSource, targetVolume);
        }

        private IEnumerator UpdateMusicWithCrossFade(AudioSourceController originalSource, AudioSourceController newSource, float transitionDuration)
        {
            float targetVolume = CurrentMusicVolume;
            for (float t = 0.0f; t <= transitionDuration; t += Time.deltaTime) {
                float delta = t / transitionDuration;
                float currentVolume = Mathf.Lerp(0, targetVolume, delta);
                SetMusicVolume(originalSource, targetVolume - currentVolume);
                SetMusicVolume(newSource, currentVolume);
                yield return null;
            }

            originalSource.Stop();
        }

        #endregion
    }
}