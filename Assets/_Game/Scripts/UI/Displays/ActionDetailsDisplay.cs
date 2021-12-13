using System;
using Scripts.Data;
using Scripts.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Displays
{
    public class ActionDetailsDisplay : MonoBehaviour
    {
        [SerializeField] private IconData _icons;
        [SerializeField] private GameData _gameData;
        [SerializeField] private TextMeshProUGUI _actionTitle;
        [SerializeField] private TextMeshProUGUI _actionDesc;
        [SerializeField] private TextMeshProUGUI _actionCost;
        [SerializeField] private GameObject _actionCostObject;
        [SerializeField] private GameObject _confirmButton;
        [SerializeField] private GameObject _cancelButton;
        [SerializeField] private Image _tileIcon;
        [SerializeField] private Image _resourceIcon;

        public static Action<TileType> OnUpdateHover;

        private void OnDisable()
        {
            OnUpdateHover.Invoke(TileType.None);
        }

        public void ShowPlacingTile(TileType tile, int cost, bool patent)
        {
            _actionCostObject.SetActive(true);
            _confirmButton.SetActive(false);
            _cancelButton.SetActive(!patent);
            _actionTitle.text = "Placing " + tile + " Tile";
            _actionDesc.text = tile switch
            {
                TileType.Ocean  => "(Any open blue highlighted tile)",
                TileType.Forest => "(Any open tile next to owned tiles)",
                TileType.City   => "(Any open tile not near another city)",
                _               => "(Any open tile)"
            };
            _actionCost.text = "Cost: " + cost;
            _tileIcon.sprite = _icons.GetTile(tile);
            _resourceIcon.gameObject.SetActive(false);
            _tileIcon.gameObject.SetActive(true);
            OnUpdateHover?.Invoke(tile);
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
                    _actionDesc.text = "(Increases PlanetType Heat by " + _gameData.Planet.HeatLevel.StepValue + ")";
                    _resourceIcon.sprite = _icons.GetResource(ResourceType.Heat, true);
                    break;
                case StandardProjectType.HeatResidue:
                    _actionTitle.text = "Heat Residue";
                    _actionDesc.text = "(Increases PlanetType Heat by " + _gameData.Planet.HeatLevel.StepValue + ")";
                    _resourceIcon.sprite = _icons.GetResource(ResourceType.Heat, true);
                    break;
                default:
                    return;
            }
            _actionCostObject.SetActive(false);
            _confirmButton.SetActive(true);
            _cancelButton.SetActive(true);
            _resourceIcon.gameObject.SetActive(true);
            _tileIcon.gameObject.SetActive(false);
        }
    }
}