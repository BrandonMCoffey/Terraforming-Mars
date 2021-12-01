using System.Collections.Generic;
using Scripts.Enums;
using UnityEngine;
using Utility.Buttons;

namespace Scripts.UI.ModalWindows
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

        public void ToggleResourceWindow()
        {
            bool open = _openWindows.Contains(MWOption.Resources);
            OpenWindow(MWOption.Resources, !open);
        }

        public void ToggleResearchWindow()
        {
            bool open = _openWindows.Contains(MWOption.Research);
            OpenWindow(MWOption.Research, !open);
        }

        public void ToggleAwardsWindow()
        {
            bool open = _openWindows.Contains(MWOption.Awards);
            OpenWindow(MWOption.Awards, !open);
        }

        public void ToggleMilestonesWindow()
        {
            bool open = _openWindows.Contains(MWOption.Milestones);
            OpenWindow(MWOption.Milestones, !open);
        }

        [Button]
        public ModalWindow OpenWindow(MWOption option, bool open = true)
        {
            if (!_windows.ContainsKey(option)) return null;
            var window = _windows[option];
            window.gameObject.SetActive(open);
            if (open) {
                window.transform.SetAsLastSibling();
                _openWindows.Add(option);
            } else if (_openWindows.Contains(option)) {
                _openWindows.Remove(option);
            }
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