using Scripts.Utility.Audio.Systems;
using UnityEngine;

namespace Scripts.Utility.Audio
{
    [CreateAssetMenu(menuName = "Utility/Sound System/Music Event")]
    public class MusicEvent : ScriptableObject
    {
        [Header("Base Settings")]
        [SerializeField] private SfxReference _musicClip = null;
        [SerializeField] private bool _crossFade = false;
        [SerializeField] private float _fadeTime = 0;

        public void Play()
        {
            if (_musicClip == null) return;

            if (_fadeTime <= 0) {
                SoundManager.Instance.PlayMusic(_musicClip);
            } else {
                if (_crossFade) {
                    SoundManager.Instance.PlayMusicWithFade(_musicClip, _fadeTime);
                } else {
                    SoundManager.Instance.PlayMusicWithCrossFade(_musicClip, _fadeTime);
                }
            }
        }
    }
}