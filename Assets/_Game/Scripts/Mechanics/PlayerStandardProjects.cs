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
            switch (type) {
                case StandardProjectType.PowerPlant:
                    _playerData.AddResource(ResourceType.Energy, 1);
                    _playerData.RemoveResource(ResourceType.Credits, cost);
                    return;
                case StandardProjectType.Asteroid:
                    GameController.Instance.IncreasePlanetStatus(PlanetStatusType.Heat);
                    _playerData.RemoveResource(ResourceType.Credits, cost);
                    return;
                case StandardProjectType.Aquifer:
                    OnPlaceTile(TileType.Ocean);
                    break;
                case StandardProjectType.Greenery:
                    OnPlaceTile(TileType.Forest);
                    break;
                case StandardProjectType.City:
                    OnPlaceTile(TileType.City);
                    break;
            }
            _currentProject = type;
        }

        #region Placing Tiles

        private void OnPlaceTile(TileType type)
        {
            _tileToPlace = type;
            HexTile.OnTileClicked += OnClickTile;
            GameController.OnCancelPlacingTile += CancelPlacingTile;
            GameController.Instance.ShowPlacingTile(type);
        }

        private void CancelPlacingTile()
        {
            _tileToPlace = TileType.None;
            HexTile.OnTileClicked -= OnClickTile;
            GameController.OnCancelPlacingTile -= CancelPlacingTile;
            GameController.Instance.ShowActions();
        }

        private void OnClickTile(HexTile tile)
        {
            if (_tileToPlace == TileType.None) return;
            if (tile.Claimed) return;
            if (tile.WaterTile != (_tileToPlace == TileType.Ocean)) return;
            PurchaseTile(_tileToPlace);
            tile.SetTile(_tileToPlace, _playerData.PlayerColor);
            CancelPlacingTile();
        }

        private void PurchaseTile(TileType tile)
        {
            int cost = StandardProjects.GetCost(_currentProject);
            bool success = _playerData.RemoveResource(ResourceType.Credits, cost);
            if (!success) {
                Debug.Log("MAJOR ERROR: ATTEMPTING TO PLACE TILE AND DOES NOT HAVE ENOUGH MONEY!");
                return;
            }
            switch (tile) {
                case TileType.Ocean:
                    GameController.Instance.IncreasePlanetStatus(PlanetStatusType.Water);
                    break;
                case TileType.Forest:
                    GameController.Instance.IncreasePlanetStatus(PlanetStatusType.Oxygen);
                    break;
            }
            OnPerformAction?.Invoke();
        }

        #endregion
    }
}