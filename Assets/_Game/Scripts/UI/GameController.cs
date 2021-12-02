using System;
using Scripts.Data;
using Scripts.Enums;
using Scripts.UI.Displays;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;

        [SerializeField] private GameData _gameData;
        [SerializeField] private PlanetData _planet;
        [SerializeField] private IconData _icons;

        [Header("Projects and Patents")]
        [SerializeField] private ActionContentFiller _actionContentFiller;
        [SerializeField] private GameObject _leftSideMain;
        [Header("Action Display")]
        [SerializeField] private GameObject _leftSideActions;
        [SerializeField] private TextMeshProUGUI _actionTitle;
        [SerializeField] private TextMeshProUGUI _actionDesc;
        [SerializeField] private TextMeshProUGUI _actionCost;
        [SerializeField] private GameObject _actionCostObject;
        [SerializeField] private GameObject _confirmButton;
        [SerializeField] private Image _tileIcon;
        [SerializeField] private Image _resourceIcon;
        [Header("Patent Details Menu")]
        [SerializeField] private GameObject _leftSidePatents;
        [SerializeField] private TextMeshProUGUI _patentTitle;
        [SerializeField] private TextMeshProUGUI _patentCost;
        [SerializeField] private GameObject _sellPatentButton;
        [SerializeField] private GameObject _activatePatentButton;

        public static Action OnConfirmAction;
        public static Action OnCancelAction;
        public static Action OnUpdateHover;

        private PatentData _currentPatent;

        public TileType TileToPlace { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            _gameData.Player.OnTurnStart += ShowActions;
            _gameData.Opponent.OnTurnStart += ShowActions;
        }

        private void OnDisable()
        {
            _gameData.Player.OnTurnStart -= ShowActions;
            _gameData.Opponent.OnTurnStart -= ShowActions;
        }

        public void IncreasePlanetStatus(PlanetStatusType type)
        {
            _planet.IncreaseStatus(type);
        }

        public void ShowPlacingTile(TileType tile, int cost)
        {
            _currentPatent = null;
            _leftSideMain.SetActive(false);
            _leftSideActions.SetActive(true);
            _leftSidePatents.SetActive(false);
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
            _currentPatent = null;
            _leftSideMain.SetActive(false);
            _leftSideActions.SetActive(true);
            _leftSidePatents.SetActive(false);
            _actionCostObject.SetActive(false);
            _confirmButton.SetActive(true);
            _resourceIcon.gameObject.SetActive(true);
            _tileIcon.gameObject.SetActive(false);
        }

        public void ShowPatentDetails(PatentData patent, bool sell)
        {
            _currentPatent = patent;
            _leftSideMain.SetActive(false);
            _leftSideActions.SetActive(false);
            _leftSidePatents.SetActive(true);

            _patentTitle.text = patent.Name;
            _patentCost.text = "Cost: " + patent.Cost;

            _sellPatentButton.SetActive(sell);
            _activatePatentButton.SetActive(!sell);
        }


        public void ShowSellPatents()
        {
            ShowActions();
            _actionContentFiller.Fill(ActionCategory.SellPatents);
        }

        public void ShowActions()
        {
            _currentPatent = null;
            _leftSideMain.SetActive(true);
            _leftSideActions.SetActive(false);
            _leftSidePatents.SetActive(false);
            TileToPlace = TileType.None;
            _actionContentFiller.Fill(ActionCategory.StandardProject);
            OnUpdateHover?.Invoke();
        }

        public void OnActivatePatent()
        {
            if (_currentPatent == null) return;
            _gameData.CurrentPlayer.ActivatePatent(_currentPatent);
            _currentPatent = null;
            ShowActions();
            _actionContentFiller.Fill(ActionCategory.OwnedPatents);
        }

        public void OnSellPatent()
        {
            if (_currentPatent == null) return;
            _gameData.CurrentPlayer.SellPatent(_currentPatent);
            _currentPatent = null;
            ShowActions();
        }

        public void ConfirmAction()
        {
            OnConfirmAction?.Invoke();
        }

        public void CancelAction()
        {
            OnCancelAction?.Invoke();
            ShowActions();
        }
    }
}