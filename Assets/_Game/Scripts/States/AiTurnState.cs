using System;
using Scripts.Data;
using Scripts.Enums;
using Scripts.Grid;
using Scripts.Mechanics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.States
{
    public class AiTurnState : State
    {
        private PlayerStandardProjects _standardProjects;

        private float _waitEndTime;
        private float _endTurnTime;

        // Reference Difficulty here
        private PlayerData _playerData;

        public override void Enter()
        {
            _standardProjects ??= new PlayerStandardProjects(_playerData);
            _waitEndTime = Time.time + Random.Range(4f, 6f);
            _endTurnTime = Time.time + 7f;
            _standardProjects.PlayerCanAct(true);
            _playerData.StartTurn();
        }

        public override void Tick()
        {
            if (Time.time > _waitEndTime) {
                Act();
            }
            if (Time.time > _endTurnTime) {
                OnEndTurn();
            }
        }

        public override void Exit()
        {
            _standardProjects.PlayerCanAct(false);
            _playerData.EndTurn();
        }

        private void OnEndTurn()
        {
            StateMachine.NextTurn();
        }

        public void Setup(PlayerData playerData)
        {
            _playerData = playerData;
        }

        private void Act()
        {
            switch (_playerData.AiLevel) {
                case AiDifficultyLevels.None:
                    return;
                case AiDifficultyLevels.Easy:
                    var rand1 = UnityEngine.Random.Range(0, 100);
                    if (rand1 > 60) {
                        return;
                    }
                    break;
                case AiDifficultyLevels.Medium:
                    var rand2 = UnityEngine.Random.Range(0, 100);
                    if (rand2 > 90) {
                        return;
                    }
                    break;
            }
            var rand = Random.Range(0, 9);
            var tiles = FindObjectsOfType<HexTile>();
            var tile = tiles[Random.Range(0, tiles.Length)];
            if (tile.Claimed) return;
            switch (rand) {
                case 0:
                    _standardProjects.OnAutoProject(StandardProjectType.PowerPlant);
                    break;
                case 1:
                    _standardProjects.OnAutoProject(StandardProjectType.Asteroid);
                    break;
                case 2:
                    if (tile.WaterTile) return;
                    _standardProjects.OnAutoProject(StandardProjectType.Aquifer, tile);
                    break;
                case 3:
                    if (tile.WaterTile) return;
                    _standardProjects.OnAutoProject(StandardProjectType.Greenery, tile);
                    break;
                case 4:
                    if (tile.WaterTile) return;
                    _standardProjects.OnAutoProject(StandardProjectType.City, tile);
                    break;
                default:
                    _playerData.ActivateFirstPatent(StateMachine.GameData);
                    break;
            }
        }
    }
}