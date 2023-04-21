using UnityEngine;

namespace Utility.Other
{
    public class DebugDisabler : MonoBehaviour
    {
        [SerializeField] private bool _debugMode = true;
        private bool _unityDebugMode = true;

        public bool DebugMode
        {
            get => _debugMode;
            set
            {
                SetDebugMode(value);
                _debugMode = value;
            }
        }

        private void Awake()
        {
            SetDebugMode(_debugMode);
        }

        private void OnValidate()
        {
            SetDebugMode(_debugMode);
        }


        private void SetDebugMode(bool mode)
        {
#if UNITY_EDITOR
            if (mode != _unityDebugMode) {
                Debug.unityLogger.logEnabled = mode;
                _unityDebugMode = mode;
            }
#else
            Debug.unityLogger.logEnabled = false;
#endif
        }
    }
}