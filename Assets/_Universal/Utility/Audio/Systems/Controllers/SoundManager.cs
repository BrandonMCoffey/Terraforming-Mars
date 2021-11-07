using UnityEngine;
using UnityEngine.Audio;
using Utility.Audio.Systems.Base;
using Utility.Pooling;

namespace Utility.Audio.Systems.Controllers
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private bool _scenePersistent = false;
        [Header("Music Controller")]
        //[SerializeField] private AudioMixerGroup _musicGroup = null;
        [SerializeField] private MusicManager _musicSourceController;
        [Header("Audio Pool")]
        [SerializeField] private AudioMixerGroup _sfxGroup = null;
        [SerializeField] private Transform _poolParent;
        [SerializeField] private int _initialPoolSize = 5;

        private PoolManager<AudioSourceController> _poolManager = new PoolManager<AudioSourceController>();
        private const string DefaultManagerName = "Audio Manager";
        private const string DefaultMusicManagerName = "Music Manager";
        private const string DefaultMusicPlayerName = "Music Player";
        private const string DefaultSfxPoolName = "SFX Pool";
        private const string DefaultSfxPlayerName = "SFX Player";

        public AudioMixerGroup SfxGroup => _sfxGroup;

        #region Singleton

        private static SoundManager _instance;

        public static SoundManager Instance
        {
            get
            {
                if (_instance == null) {
                    _instance = FindObjectOfType<SoundManager>();
                    if (_instance == null) {
                        _instance = new GameObject(DefaultManagerName, typeof(SoundManager)).GetComponent<SoundManager>();
                    }
                }
                return _instance;
            }
        }

        private void Awake()
        {
            transform.SetParent(null);
            if (_instance == null) {
                if (_scenePersistent) {
                    DontDestroyOnLoad(gameObject);
                }
                _instance = this;
            } else if (_instance != this) {
                Destroy(gameObject);
            }

            #endregion

            // Create Music Manager
            if (_musicSourceController == null) {
                _musicSourceController = new GameObject(DefaultMusicManagerName, typeof(MusicManager)).GetComponent<MusicManager>();
                _musicSourceController.transform.SetParent(transform);
            }
            _musicSourceController.BuildMusicPlayers(DefaultMusicPlayerName);
            // Create SFX Pool
            if (_poolParent == null) {
                Transform pool = new GameObject(DefaultSfxPoolName).transform;
                pool.SetParent(transform);
                _poolParent = pool;
            }
            _poolManager.BuildInitialPool(_poolParent, DefaultSfxPlayerName, _initialPoolSize);
        }

        #region Music Manager

        public void PlayMusic(SfxReference track)
        {
            if (track.NullTest()) return;
            _musicSourceController.PlayMusic(track);
        }

        public void PlayMusicWithFade(SfxReference track, float fadeTime)
        {
            if (track.NullTest()) return;
            _musicSourceController.PlayMusicWithFade(track, fadeTime);
        }

        public void PlayMusicWithCrossFade(SfxReference track, float fadeTime)
        {
            if (track.NullTest()) return;
            _musicSourceController.PlayMusicWithCrossFade(track, fadeTime);
        }

        #endregion

        public AudioSourceController GetController()
        {
            var source = _poolManager.GetObject();
            source.ResetSource();
            return source;
        }

        public void ReturnController(AudioSourceController sourceController)
        {
            sourceController.ResetSource();
            _poolManager.ReturnObject(sourceController);
        }
    }
}