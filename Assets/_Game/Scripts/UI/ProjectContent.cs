using Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class ProjectContent : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData = null;
        [SerializeField] private StandardProjectType _project;
        [SerializeField] private Button _button = null;
        [SerializeField] private TextMeshProUGUI _titleText = null;
        [SerializeField] private TextMeshProUGUI _costText = null;

        private bool _active;

        private void Start()
        {
            if (_playerData != null) {
                _playerData.OnAnythingChanged += UpdateButton;
            }
            _button.onClick.AddListener(delegate { StandardProjects.InvokeProject(_project); });
        }

        private void OnValidate()
        {
            _titleText.text = StandardProjects.GetName(_project);
            _costText.text = StandardProjects.GetCostReadable(_project);
        }

        public void SetInteractable(bool active)
        {
            _active = active;
            UpdateButton();
        }

        private void UpdateButton()
        {
            if (_project == StandardProjectType.SellPatents) {
                _button.interactable = _active && _playerData.HasAvailablePatents();
            } else {
                int cost = StandardProjects.GetCost(_project);
                _button.interactable = _active && _playerData.HasCredits(cost);
            }
        }
    }
}