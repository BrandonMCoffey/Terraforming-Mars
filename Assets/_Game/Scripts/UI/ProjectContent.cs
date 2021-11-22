using System;
using Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class ProjectContent : MonoBehaviour
    {
        [SerializeField] private StandardProjectType _project = StandardProjectType.SellPatents;
        [Header("References")]
        [SerializeField] private PlayerData _playerData = null;
        [SerializeField] private IconData _iconData = null;
        [SerializeField] private Button _button = null;
        [SerializeField] private TextMeshProUGUI _titleText = null;
        [Header("Cost and Effect")]
        [SerializeField] private Image _costImage = null;
        [SerializeField] private TextMeshProUGUI _costText = null;
        [SerializeField] private Image _effectImage = null;
        [SerializeField] private TextMeshProUGUI _effectText = null;

        private bool _active;

        private void Start()
        {
            if (_playerData != null) {
                _playerData.OnAnythingChanged += UpdateButton;
            }
            _button.onClick.AddListener(delegate { StandardProjects.InvokeProject(_project); });
            UpdateVisuals();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _titleText.text = StandardProjects.GetName(_project);
            if (_project == StandardProjectType.SellPatents) {
                _costText.gameObject.SetActive(false);
                _effectText.gameObject.SetActive(true);
                _effectText.text = "+1";
            } else {
                _costText.gameObject.SetActive(true);
                _effectText.gameObject.SetActive(false);
                _costText.text = StandardProjects.GetCostReadable(_project);
            }
            UpdateVisuals();
        }
#endif

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

        private void UpdateVisuals()
        {
            switch (_project) {
                case StandardProjectType.SellPatents:
                    _costImage.sprite = _iconData.GetResource(ResourceType.Null);
                    _effectImage.sprite = _iconData.GetResource(ResourceType.Credits, true);
                    break;
                case StandardProjectType.PowerPlant:
                    _costImage.sprite = _iconData.GetResource(ResourceType.Credits, true);
                    _effectImage.sprite = _iconData.GetResource(ResourceType.Energy);
                    break;
                case StandardProjectType.Asteroid:
                    _costImage.sprite = _iconData.GetResource(ResourceType.Credits, true);
                    _effectImage.sprite = _iconData.GetResource(ResourceType.Heat);
                    break;
                case StandardProjectType.Aquifer:
                    _costImage.sprite = _iconData.GetResource(ResourceType.Credits, true);
                    _effectImage.sprite = _iconData.GetTile(TileType.Ocean);
                    break;
                case StandardProjectType.Greenery:
                    _costImage.sprite = _iconData.GetResource(ResourceType.Credits, true);
                    _effectImage.sprite = _iconData.GetTile(TileType.Forest);
                    break;
                case StandardProjectType.City:
                    _costImage.sprite = _iconData.GetResource(ResourceType.Credits, true);
                    _effectImage.sprite = _iconData.GetTile(TileType.City);
                    break;
            }
        }
    }
}