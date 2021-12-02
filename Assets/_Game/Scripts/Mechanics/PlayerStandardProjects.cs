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
        private static event Action<TileType> OnForcePlaceTile;
        private static event Action<TileType> OnAutoPlaceTile;

        private PlayerData _playerData;
        private StandardProjectType _currentProject;
        private TileType _tileToPlace = TileType.None;

        private bool _patentTile = false;

        #region Player Turn State

        public PlayerStandardProjects(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public void PlayerCanAct(bool canAct)
        {
            if (canAct) {
                StandardProjects.OnUseProject += OnStandardProject;
                OnForcePlaceTile += PatentPlaceTile;
                OnAutoPlaceTile += PatentAutoPlaceTile;
            } else {
                StandardProjects.OnUseProject -= OnStandardProject;
                OnForcePlaceTile -= PatentPlaceTile;
                OnAutoPlaceTile -= PatentAutoPlaceTile;
            }
        }

        #endregion

        public void OnStandardProject(StandardProjectType type)
        {
            _patentTile = false;
            // Sell Patents
            if (type == StandardProjectType.SellPatents) {
                GameController.Instance.ShowSellPatents();
                return;
            }
            // Check cost
            var cost = StandardProjects.GetCost(type);
            var costType = StandardProjects.GetCostType(type);
            if (!_playerData.HasResource(costType, cost)) return;
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
                case StandardProjectType.Plants:
                    OnStartPlacingTile(TileType.Forest);
                    break;
                case StandardProjectType.HeatResidue:
                    OnStartProject();
                    break;
            }
        }

        public void OnAutoProject(StandardProjectType type)
        {
            _patentTile = false;
            if (type == StandardProjectType.SellPatents) {
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
                    OnConfirmProject();
                    GameController.Instance.ShowActions();
                    return;
                case StandardProjectType.Asteroid:
                    OnStartProject();
                    OnConfirmProject();
                    GameController.Instance.ShowActions();
                    return;
                case StandardProjectType.Aquifer:
                    AutoPlaceTile(TileType.Ocean);
                    break;
                case StandardProjectType.Greenery:
                    AutoPlaceTile(TileType.Forest);
                    break;
                case StandardProjectType.City:
                    AutoPlaceTile(TileType.City);
                    break;
                case StandardProjectType.Plants:
                    AutoPlaceTile(TileType.Forest);
                    break;
                case StandardProjectType.HeatResidue:
                    OnStartProject();
                    OnConfirmProject();
                    GameController.Instance.ShowActions();
                    break;
            }
        }

        public void PatentAutoPlaceTile(TileType type)
        {
            _patentTile = true;
            AutoPlaceTile(type);
        }

        public void AutoPlaceTile(TileType type)
        {
            OnStartPlacingTile(type);
            OnClickTile(RandomTile.Instance.GetRandomTile(type));
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
            var cost = StandardProjects.GetCost(_currentProject);
            var costType = StandardProjects.GetCostType(_currentProject);
            _playerData.RemoveResource(costType, cost);
            switch (_currentProject) {
                case StandardProjectType.PowerPlant:
                    _playerData.AddResource(ResourceType.Energy, 1, true);
                    _playerData.SoundData.PowerPlantSfx.Play();
                    return;
                case StandardProjectType.Asteroid:
                    IncreasePlanetStatus(PlanetStatusType.Heat);
                    _playerData.SoundData.AsteroidSfx.Play();
                    return;
                case StandardProjectType.HeatResidue:
                    IncreasePlanetStatus(PlanetStatusType.Heat);
                    _playerData.SoundData.AsteroidSfx.Play();
                    break;
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

        public static void ForcePlaceTile(TileType type, bool auto)
        {
            if (auto) {
                OnAutoPlaceTile?.Invoke(type);
            } else {
                OnForcePlaceTile?.Invoke(type);
            }
        }

        private void PatentPlaceTile(TileType type)
        {
            _patentTile = true;
            OnStartPlacingTile(type);
        }

        private void OnStartPlacingTile(TileType type)
        {
            _tileToPlace = type;
            HexTile.OnTileClicked += OnClickTile;
            GameController.OnCancelAction += CancelPlacingTile;
            GameController.Instance.ShowPlacingTile(type, StandardProjects.GetCost(_currentProject), _patentTile);
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
            if (tile == null || tile.Claimed || _tileToPlace == TileType.None) {
                return;
            }
            if (tile.WaterTile != (_tileToPlace == TileType.Ocean)) {
                Debug.Log("Error placing tile: " + _tileToPlace + " Water Tiles reserved for Oceans Only");
                return;
            }
            if (_tileToPlace == TileType.City && tile.HasAdjacentCity) {
                Debug.Log("Error placing tile: " + _tileToPlace + " Cities cannot be near other cities");
                return;
            }
            PurchaseTile(tile, _tileToPlace);
            CancelPlacingTile();
        }

        private void PurchaseTile(HexTile tile, TileType tileType)
        {
            if (!_patentTile) {
                var cost = StandardProjects.GetCost(_currentProject);
                var costType = StandardProjects.GetCostType(_currentProject);
                bool success = _playerData.RemoveResource(costType, cost);
                if (!success) {
                    Debug.Log("MAJOR ERROR: ATTEMPTING TO PLACE TILE AND DOES NOT HAVE ENOUGH MONEY!");
                    return;
                }
            }
            int bonus = tile.SetTile(tileType, _playerData.PlayerColor);
            _playerData.AddResource(ResourceType.Credits, bonus);
            switch (tileType) {
                case TileType.Ocean:
                    _playerData.AddOwnedTile(tile);
                    IncreasePlanetStatus(PlanetStatusType.Water);
                    break;
                case TileType.Forest:
                    _playerData.AddOwnedTile(tile);
                    IncreasePlanetStatus(PlanetStatusType.Oxygen);
                    break;
                default:
                    _playerData.AddOwnedTile(tile);
                    break;
            }
            OnPerformAction?.Invoke();
        }

        private void IncreasePlanetStatus(PlanetStatusType type)
        {
            if (GameController.Instance.IncreasePlanetStatus(type)) {
                _playerData.AddHonor(1);
            }
        }

        #endregion
    }
}