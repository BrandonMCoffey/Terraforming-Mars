using System;
using Scripts.Data;
using Scripts.Enums;
using Scripts.Grid;
using Scripts.UI;
using UnityEngine;

namespace Scripts.Mechanics
{
    public class PlayerStandardProjects
    {
        public event Action OnPerformAction;

        private PlayerData _playerData;
        private StandardProjectType _currentProject;
        private TileType _tileToPlace = TileType.None;

        #region Player Turn State

        public PlayerStandardProjects(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public void PlayerCanAct(bool canAct)
        {
            if (canAct) {
                StandardProjects.OnUseProject += OnStandardProject;
            } else {
                StandardProjects.OnUseProject -= OnStandardProject;
            }
        }

        #endregion

        private void OnStandardProject(StandardProjectType type)
        {
            // Sell Patents
            if (type == StandardProjectType.SellPatents) {
                GameController.Instance.ShowSellPatents();
                return;
            }
            // Check cost
            int cost = StandardProjects.GetCost(type);
            if (!_playerData.HasResource(ResourceType.Credits, cost)) return;
            // Run standard project
            _currentProject = type;
            switch (type) {
                case StandardProjectType.PowerPlant:
                    OnStartProject();
                    return;
                case StandardProjectType.Asteroid:
                    OnStartProject();
                    return;
                case StandardProjectType.Aquifer:
                    OnStartPlacingTile(TileType.Ocean);
                    break;
                case StandardProjectType.Greenery:
                    OnStartPlacingTile(TileType.Forest);
                    break;
                case StandardProjectType.City:
                    OnStartPlacingTile(TileType.City);
                    break;
            }
        }

        #region Power Plant and Asteroid

        private void OnStartProject()
        {
            GameController.OnConfirmAction += OnConfirmProject;
            GameController.OnCancelAction += OnCancelProject;
            GameController.Instance.ShowStandardProject(_currentProject);
        }

        private void OnConfirmProject()
        {
            OnCancelProject();
            int cost = StandardProjects.GetCost(_currentProject);
            switch (_currentProject) {
                case StandardProjectType.PowerPlant:
                    _playerData.AddResource(ResourceType.Energy, 1);
                    _playerData.RemoveResource(ResourceType.Credits, cost);
                    return;
                case StandardProjectType.Asteroid:
                    IncreasePlanetStatus(PlanetStatusType.Heat);
                    _playerData.RemoveResource(ResourceType.Credits, cost);
                    return;
                default:
                    Debug.Log("MAJOR ERROR: CONFIRMING PROJECT WHEN CURRENT PROJECT IS INVALID!");
                    return;
            }
        }

        private void OnCancelProject()
        {
            GameController.OnConfirmAction -= OnConfirmProject;
            GameController.OnCancelAction -= OnCancelProject;
            GameController.Instance.ShowActions();
        }

        #endregion

        #region Placing Tiles

        private void OnStartPlacingTile(TileType type)
        {
            _tileToPlace = type;
            HexTile.OnTileClicked += OnClickTile;
            GameController.OnCancelAction += CancelPlacingTile;
            GameController.Instance.ShowPlacingTile(type, StandardProjects.GetCost(_currentProject));
        }

        private void CancelPlacingTile()
        {
            _tileToPlace = TileType.None;
            HexTile.OnTileClicked -= OnClickTile;
            GameController.OnCancelAction -= CancelPlacingTile;
            GameController.Instance.ShowActions();
        }

        private void OnClickTile(HexTile tile)
        {
            if (_tileToPlace == TileType.None) return;
            if (tile.Claimed) return;
            if (tile.WaterTile != (_tileToPlace == TileType.Ocean)) return;
            if (_tileToPlace == TileType.City && tile.HasAdjacentCity) return;
            PurchaseTile(tile, _tileToPlace);
            CancelPlacingTile();
        }

        private void PurchaseTile(HexTile tile, TileType tileType)
        {
            int cost = StandardProjects.GetCost(_currentProject);
            bool success = _playerData.RemoveResource(ResourceType.Credits, cost);
            if (!success) {
                Debug.Log("MAJOR ERROR: ATTEMPTING TO PLACE TILE AND DOES NOT HAVE ENOUGH MONEY!");
                return;
            }
            int bonus = tile.SetTile(tileType, _playerData.PlayerColor);
            _playerData.AddResource(ResourceType.Credits, bonus);
            switch (tileType) {
                case TileType.Ocean:
                    IncreasePlanetStatus(PlanetStatusType.Water);
                    break;
                case TileType.Forest:
                    IncreasePlanetStatus(PlanetStatusType.Oxygen);
                    break;
            }
            OnPerformAction?.Invoke();
        }

        private void IncreasePlanetStatus(PlanetStatusType type)
        {
            _playerData.AddHonor(1);
            GameController.Instance.IncreasePlanetStatus(type);
        }

        #endregion
    }
}