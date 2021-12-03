using Scripts.Data;
using Scripts.Enums;
using Scripts.Mechanics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Displays
{
    public class ProjectContent : MonoBehaviour
    {
        [SerializeField] private StandardProjectType _project = StandardProjectType.SellPatents;
        [Header("References")]
        [SerializeField] private GameData _gameData;
        [SerializeField] private IconData _iconData;
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _titleText;
        [Header("Cost and Effect")]
        [SerializeField] private Image _costImage;
        [SerializeField] private TextMeshProUGUI _costText;
        [SerializeField] private Image _effectImage;
        [SerializeField] private TextMeshProUGUI _effectText;

        private bool _active;
        private PlayerData _player;

        private void OnEnable()
        {
            if (_gameData == null) return;
            _player = _gameData.CurrentPlayer;
            _player.OnPatentsChanged += UpdateButton;
            _player.OnResourcesChanged += UpdateButton;
            UpdateButton();
        }

        private void OnDisable()
        {
            if (_player == null) return;
            _player.OnPatentsChanged -= UpdateButton;
            _player.OnResourcesChanged -= UpdateButton;
        }

        private void Start()
        {
            _button.onClick.AddListener(delegate { StandardProjects.InvokeProject(_project); });
            UpdateVisuals();
        }

        public void SetInteractable(bool active)
        {
            _active = active;
            UpdateButton();
        }

        private void UpdateButton()
        {
            if (_button == null) return;
            if (_project == StandardProjectType.SellPatents) {
                _button.interactable = _active && _gameData.CurrentPlayer.UserControlled && _gameData.CurrentPlayer.HasAvailablePatents();
            } else {
                var cost = StandardProjects.GetCost(_project);
                var costType = StandardProjects.GetCostType(_project);
                _button.interactable = _active && _gameData.CurrentPlayer.UserControlled && _gameData.CurrentPlayer.HasResource(costType, cost);
            }
        }

        private void UpdateVisuals()
        {
            _titleText.text = StandardProjects.GetName(_project);
            _costText.gameObject.SetActive(true);
            _costImage.gameObject.SetActive(true);
            _costImage.sprite = _iconData.GetResource(StandardProjects.GetCostType(_project), true);
            _effectText.gameObject.SetActive(false);
            _costText.text = StandardProjects.GetCostReadable(_project);
            switch (_project) {
                case StandardProjectType.SellPatents:
                    _costText.gameObject.SetActive(false);
                    _costImage.gameObject.SetActive(false);
                    _effectText.gameObject.SetActive(true);
                    _effectText.text = "+1";
                    _effectImage.sprite = _iconData.GetResource(ResourceType.Credits, true);
                    break;
                case StandardProjectType.PowerPlant:
                    _effectImage.sprite = _iconData.GetResource(ResourceType.Energy);
                    break;
                case StandardProjectType.Asteroid:
                    _effectImage.sprite = _iconData.GetResource(ResourceType.Heat);
                    break;
                case StandardProjectType.Aquifer:
                    _effectImage.sprite = _iconData.GetTile(TileType.Ocean);
                    break;
                case StandardProjectType.Greenery:
                    _effectImage.sprite = _iconData.GetTile(TileType.Forest);
                    break;
                case StandardProjectType.City:
                    _effectImage.sprite = _iconData.GetTile(TileType.City);
                    break;
                case StandardProjectType.Plants:
                    _effectImage.sprite = _iconData.GetTile(TileType.Forest);
                    break;
                case StandardProjectType.HeatResidue:
                    _effectImage.sprite = _iconData.GetScience(ScienceType.Mars);
                    break;
            }
        }
    }
}