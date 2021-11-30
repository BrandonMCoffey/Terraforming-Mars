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

        public void StartPlayerTurn()
        {
            StandardProjects.OnUseProject += OnStandardProject;
        }

        public void EndPlayerTurn()
        {
            StandardProjects.OnUseProject -= OnStandardProject;
        }

        #endregion

        private void OnStandardProject(StandardProjectType type)
        {
            _currentProject = type;
            switch (type) {
                case StandardProjectType.SellPatents:
                    OnSellPatents();
                    break;
                case StandardProjectType.PowerPlant:
                    OnPowerPlant();
                    break;
                case StandardProjectType.Asteroid:
                    OnAsteroid();
                    break;
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
        }

        #region Sell Patents

        private static void OnSellPatents()
        {
            CanvasController.Instance.ShowSellPatents();
        }

        #endregion

        #region Planet Effects

        private void OnPowerPlant()
        {
        }

        private void OnAsteroid()
        {
        }

        #endregion

        #region Placing Tiles

        private void OnPlaceTile(TileType type)
        {
            _tileToPlace = type;
            HexTile.OnTileClicked += OnClickTile;
            CanvasController.OnCancelPlacingTile += CancelPlacingTile;
            CanvasController.Instance.ShowPlacingTile(type);
        }

        private void CancelPlacingTile()
        {
            _tileToPlace = TileType.None;
            HexTile.OnTileClicked -= OnClickTile;
            CanvasController.OnCancelPlacingTile -= CancelPlacingTile;
            CanvasController.Instance.ShowActions();
        }

        private void OnClickTile(HexTile tile)
        {
            if (_tileToPlace == TileType.None) return;
            if (tile.Claimed) return;
            tile.SetTile(_tileToPlace, _playerData.PlayerColor);
            PurchaseTile();
            CancelPlacingTile();
        }

        private void PurchaseTile()
        {
            int cost = StandardProjects.GetCost(_currentProject);
            _playerData.RemoveResource(ResourceType.Credits, cost);
            OnPerformAction?.Invoke();
        }

        #endregion
    }
}