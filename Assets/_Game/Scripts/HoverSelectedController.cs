using UnityEngine;

namespace Scripts
{
    public class HoverSelectedController : MonoBehaviour
    {
        [SerializeField] private GameObject _hoverArt = null;
        [SerializeField] private GameObject _selectedArt = null;

        public static HoverSelectedController instance;

        private Transform _hoverParent;
        private Transform _selectedParent;
        private bool _hoverActive;
        private bool _selectedActive;

        private void Awake()
        {
            if (instance != null && this != instance) {
                Destroy(gameObject);
            }
            instance = this;
        }

        private void Start()
        {
            DisableHover(true);
            DisableSelected(true);
        }

        public void Update()
        {
            if (_hoverActive) {
                _hoverArt.transform.SetPositionAndRotation(_hoverParent.position, _hoverParent.rotation);
            }
            if (_selectedActive) {
                _selectedArt.transform.SetPositionAndRotation(_selectedParent.position, _selectedParent.rotation);
            }
        }

        public void SetHoverParent(Transform parent)
        {
            if (_selectedParent == parent) {
                if (_hoverActive) {
                    _hoverArt.SetActive(false);
                    _hoverActive = false;
                }
                return;
            }
            if (!_hoverActive) {
                _hoverArt.SetActive(true);
                _hoverActive = true;
            }
            _hoverParent = parent;
        }

        public void DisableHover(bool force = false)
        {
            if (force || _hoverActive) {
                _hoverParent = null;
                _hoverArt.SetActive(false);
                _hoverActive = false;
            }
        }

        public void SetSelectedParent(Transform parent)
        {
            if (!_selectedActive) {
                _selectedArt.SetActive(true);
                _selectedActive = true;
                _hoverArt.SetActive(false);
                _hoverActive = false;
            }
            _selectedParent = parent;
        }

        public void DisableSelected(bool force = false)
        {
            if (force || _selectedActive) {
                _selectedParent = null;
                _selectedArt.SetActive(false);
                _selectedActive = false;
            }
        }
    }
}