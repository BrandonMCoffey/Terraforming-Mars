using Scripts.Data;
using Scripts.Enums;
using Scripts.Mechanics;
using Scripts.UI;
using Scripts.UI.Awards;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.States
{
    public class AiTurnState : State
    {
        private PlayerStandardProjects _standardProjects;

        private int _actions = 0;
        private float _waitEndTime;
        private float _endTurnTime;

        // Reference Difficulty here
        private PlayerData _playerData;

        public override void Enter()
        {
            _actions = 0;
            _standardProjects ??= new PlayerStandardProjects(_playerData);
            _waitEndTime = Time.time + Random.Range(4f, 5f);
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
            if (_actions++ >= 3) return;
            _waitEndTime = Time.time + 0.1f;
            switch (_playerData.AiLevel) {
                case AiDifficultyLevels.None:
                    Debug.Log("AI: Dummy");
                    return;
                case AiDifficultyLevels.Easy:
                    if (_actions > 2) return;
                    var rand1 = UnityEngine.Random.Range(0, 100);
                    if (rand1 > 60) {
                        Debug.Log("AI: Do nothing");
                        return;
                    } else if (rand1 > 55) {
                        CheckAwards();
                        return;
                    }
                    break;
                case AiDifficultyLevels.Medium:
                    var rand2 = UnityEngine.Random.Range(0, 100);
                    if (rand2 > 75) {
                        Debug.Log("AI: Do nothing");
                        return;
                    }
                    break;
                case AiDifficultyLevels.Hard:
                    var rand3 = UnityEngine.Random.Range(0, 100);
                    if (rand3 > 95) {
                        Debug.Log("AI: Do nothing");
                        return;
                    }
                    CheckAwards();
                    break;
            }
            if (_playerData.HasResource(ResourceType.Heat, 15)) {
                _standardProjects.OnAutoProject(StandardProjectType.HeatResidue);
                return;
            }
            if (_playerData.HasResource(ResourceType.Plant, 15)) {
                _standardProjects.OnAutoProject(StandardProjectType.Plants);
                return;
            }
            var rand = Random.Range(0, 11);
            StandardProjectType standardProject = StandardProjectType.PowerPlant;
            switch (rand) {
                case 0:
                    break;
                case 1:
                    standardProject = StandardProjectType.Asteroid;
                    break;
                case 2:
                    standardProject = StandardProjectType.Aquifer;
                    break;
                case 3:
                    standardProject = StandardProjectType.Greenery;
                    break;
                case 4:
                    standardProject = StandardProjectType.City;
                    break;
                case 5:
                    standardProject = StandardProjectType.HeatResidue;
                    break;
                case 6:
                    standardProject = StandardProjectType.Plants;
                    break;
                case 9 when _playerData.AiLevel == AiDifficultyLevels.Medium:
                    CheckAwards();
                    return;
                default:
                    if (_playerData.ActivateFirstPatent(StateMachine.GameData)) {
                        _endTurnTime += 2;
                    } else {
                        _actions--;
                    }
                    return;
            }
            if (!CheckProject(standardProject)) {
                _actions--;
                return;
            }
            Debug.Log("AI: Standard Project (" + standardProject + ")");
            _standardProjects.OnAutoProject(standardProject);
            AnnouncementController.Instance.Announce(_playerData.PlayerName + " " + StandardProjects.GetActionTitle(standardProject), "");
            _endTurnTime += 2;
        }

        private bool CheckProject(StandardProjectType type)
        {
            return _playerData.HasResource(StandardProjects.GetCostType(type), StandardProjects.GetCost(type));
        }

        private void CheckAwards()
        {
            if (TryClaimAward(MilestoneType.Terraformer)) return;
            if (TryClaimAward(MilestoneType.Mayor)) return;
            if (TryClaimAward(MilestoneType.Gardener)) return;
            if (TryClaimAward(MilestoneType.Builder)) return;
            TryClaimAward(MilestoneType.Planner);
        }

        private bool TryClaimAward(MilestoneType type)
        {
            if (_playerData.Milestones.Contains(type)) return false;
            if (MilestoneController.CanClaimMilestone(_playerData, type)) {
                Debug.Log("AI: Claim Milestone: " + type);
                MilestoneController.Instance.gameObject.SetActive(true);
                MilestoneController.Instance.Claim(type);
                MilestoneController.Instance.gameObject.SetActive(false);
                _endTurnTime += 3;
                AnnouncementController.Instance.Announce(_playerData.PlayerName + " Claimed the " + type + "Milestone", "");
                return true;
            }
            return false;
        }
    }
}