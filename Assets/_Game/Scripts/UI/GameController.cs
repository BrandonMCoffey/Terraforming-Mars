using System;
using Scripts.Data;
using Scripts.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;

        [SerializeField] private PlanetData _planet = null;
        [SerializeField] private IconData _icons = null;

        [Header("While Placing vs Main")]
        [SerializeField] private GameObject _leftSideMain = null;
        [SerializeField] private GameObject _leftSidePlacing = null;
        [SerializeField] private TextMeshProUGUI _actionTitle = null;
        [SerializeField] private TextMeshProUGUI _actionDesc = null;
        [SerializeField] private TextMeshProUGUI _actionCost = null;
        [SerializeField] private GameObject _actionCostObject = null;
        [SerializeField] private GameObject _confirmButton = null;
        [SerializeField] private Image _tileIcon = null;
        [SerializeField] private Image _resourceIcon = null;

        public static Action OnConfirmAction;
        public static Action OnCancelAction;
        public static Action OnUpdateHover;

        public TileType TileToPlace { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void IncreasePlanetStatus(PlanetStatusType type)
        {
            _planet.IncreaseStatus(type);
        }

        public void ShowPlacingTile(TileType tile, int cost)
        {
            _leftSideMain.SetActive(false);
            _leftSidePlacing.SetActive(true);
            _actionCostObject.SetActive(true);
            _confirmButton.SetActive(false);
            TileToPlace = tile;
            _actionTitle.text = "Placing " + tile + " Tile";
            switch (tile) {
                case TileType.Ocean:
                    _actionDesc.text = "(Any open blue highlighted tile)";
                    break;
                case TileType.Forest:
                    _actionDesc.text = "(Any open tile next to owned tiles)";
                    break;
                case TileType.City:
                    _actionDesc.text = "(Any open tile not near another city)";
                    break;
                case TileType.Nuke:
                    _actionDesc.text = "(Any open tile)";
                    break;
            }
            _actionCost.text = "Cost: " + cost;
            _tileIcon.sprite = _icons.GetTile(tile);
            _resourceIcon.gameObject.SetActive(false);
            _tileIcon.gameObject.SetActive(true);
            OnUpdateHover?.Invoke();
        }

        public void ShowStandardProject(StandardProjectType type)
        {
            switch (type) {
                case StandardProjectType.PowerPlant:
                    _actionTitle.text = "Activate Power Plant";
                    _actionDesc.text = "(Increases Energy Production Level by 1)";
                    _resourceIcon.sprite = _icons.GetResource(ResourceType.Energy, true);
                    break;
                case StandardProjectType.Asteroid:
                    _actionTitle.text = "Asteroid from Space";
                    _actionDesc.text = "(Increases Planet Heat by " + _planet.HeatLevel.StepValue + ")";
                    _resourceIcon.sprite = _icons.GetResource(ResourceType.Heat, true);
                    break;
                default:
                    return;
            }
            _leftSideMain.SetActive(false);
            _leftSidePlacing.SetActive(true);
            _actionCostObject.SetActive(false);
            _confirmButton.SetActive(true);
            _resourceIcon.gameObject.SetActive(true);
            _tileIcon.gameObject.SetActive(false);
        }

        public void ShowSellPatents()
        {
        }

        public void ShowActions()
        {
            _leftSideMain.SetActive(true);
            _leftSidePlacing.SetActive(false);
            TileToPlace = TileType.None;
            OnUpdateHover?.Invoke();
        }

        public void ConfirmAction()
        {
            OnConfirmAction?.Invoke();
        }

        public void CancelAction()
        {
            OnCancelAction?.Invoke();
        }
    }
}