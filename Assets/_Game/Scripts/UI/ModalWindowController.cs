using System.Collections.Generic;
using Scripts.Enums;
using UnityEngine;
using Utility.Buttons;

namespace Scripts.UI
{
    public class ModalWindowController : MonoBehaviour
    {
        [SerializeField] private bool _isSingleton = true;
        [SerializeField] private List<MW> _modals = new List<MW>();

        public static ModalWindowController Instance;

        private Dictionary<MWOption, ModalWindow> _windows;
        private List<MWOption> _openWindows = new List<MWOption>();

        private void Awake()
        {
            if (_isSingleton) {
                Instance = this;
            }
        }

        private void Start()
        {
            _windows = new Dictionary<MWOption, ModalWindow>(_modals.Count);
            foreach (var window in _modals) {
                _windows.Add(window.Option, window.Window);
            }
            foreach (var window in _windows) {
                window.Value.gameObject.SetActive(false);
            }
        }

        public void OpenResourceWindow()
        {
            OpenWindow(MWOption.Resources);
        }

        public void OpenResearchWindow()
        {
            OpenWindow(MWOption.Research);
        }

        public void OpenAwardsWindow()
        {
            OpenWindow(MWOption.Awards);
        }

        public void OpenMilestonesWindow()
        {
            OpenWindow(MWOption.Milestones);
        }

        [Button]
        public ModalWindow OpenWindow(MWOption option)
        {
            if (!_windows.ContainsKey(option)) return null;
            var window = _windows[option];
            window.gameObject.SetActive(true);
            window.transform.SetAsLastSibling();
            _openWindows.Add(option);
            return window;
        }

        [Button]
        public void CloseAllWindows()
        {
            foreach (var option in _openWindows) {
                _windows[option].gameObject.SetActive(false);
            }
            _openWindows.Clear();
        }
    }

    [System.Serializable]
    public struct MW
    {
        public MWOption Option;
        public ModalWindow Window;
    }
}