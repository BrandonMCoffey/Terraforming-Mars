using UnityEngine;
using Utility.GameEvents.Logic;

namespace Utility.GameEvents
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioOnEvent : GameEventListener
    {
        [Tooltip("Audio to play invoke when Event is raised.")]
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private bool _oneShot = true;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public override void OnEventRaised()
        {
            base.OnEventRaised();
            if (_oneShot) {
                if (_audioClip != null) _audioSource.PlayOneShot(_audioClip);
            } else {
                if (_audioClip != null) _audioSource.clip = _audioClip;
                _audioSource.Play();
            }
        }
    }
}