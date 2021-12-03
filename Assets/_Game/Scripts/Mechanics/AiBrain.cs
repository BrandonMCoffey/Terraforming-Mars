using System;
using System.Linq;
using Scripts.Data;
using Scripts.Enums;
using Scripts.UI.Awards;

namespace Scripts.Mechanics
{
    public class AiBrain
    {
        private GameData _gameData;
        private PlayerData _playerData;
        private PlayerStandardProjects _standardProjects;
        private AiLogic _aiLogic;

        private bool _fundedRandom;
        private bool _fundedThermalist;

        private int _actions;

        public AiBrain(GameData gameData, PlayerData playerData)
        {
            _gameData = gameData;
            _playerData = playerData;
            _standardProjects = new PlayerStandardProjects(playerData);
            _aiLogic = playerData.AiLevel switch {
                AiDifficultyLevels.None   => null,
                AiDifficultyLevels.Easy   => gameData.EasyAiLogic,
                AiDifficultyLevels.Medium => gameData.MediumAiLogic,
                AiDifficultyLevels.Hard   => gameData.HardAiLogic,
                _                         => _aiLogic
            };
        }


        public void PlayerCanAct(bool canAct)
        {
            _standardProjects.PlayerCanAct(canAct);
            if (canAct) {
                _actions = 0;
                _playerData.StartTurn();
            } else {
                _playerData.EndTurn();
            }
        }

        public bool Think()
        {
            if (_actions++ > _aiLogic.ActionsPerTurn) return false;
            int attempts = 0;
            bool success = false;
            AiActions action = AiActions.None;
            while (!success) {
                action = _aiLogic.GetWeightedRandom();
                success = Act(action);
                if (attempts++ > 20) {
                    break;
                }
            }
            return success && action != AiActions.None;
        }

        protected bool Act(AiActions action)
        {
            return action switch {
                AiActions.None              => true,
                AiActions.ClaimAnyMilestone => MilestoneController.Instance.TryClaimAny(_playerData),
                AiActions.FundThermalist    => FundThermalist(),
                AiActions.FundRandomAward   => FundRandom(),
                AiActions.PowerPlant        => DoStandardProject(StandardProjectType.PowerPlant),
                AiActions.Asteroid          => DoStandardProject(StandardProjectType.Asteroid),
                AiActions.Aquifer           => DoStandardProject(StandardProjectType.Aquifer),
                AiActions.Greenery          => DoStandardProject(StandardProjectType.Greenery),
                AiActions.City              => DoStandardProject(StandardProjectType.City),
                AiActions.HeatResidue       => DoStandardProject(StandardProjectType.HeatResidue),
                AiActions.Plants            => DoStandardProject(StandardProjectType.Plants),
                AiActions.FirstPatent       => _playerData.ActivateFirstPatent(_gameData),
                _                           => false
            };
        }

        private bool DoStandardProject(StandardProjectType type)
        {
            // If Heat Complete, Dont increase heat
            if (_gameData.Planet.HeatComplete && (type == StandardProjectType.HeatResidue || type == StandardProjectType.Asteroid)) return false;
            if (!_playerData.HasResource(StandardProjects.GetCostType(type), StandardProjects.GetCost(type))) return false;
            _standardProjects.OnAutoProject(type);
            return true;
        }

        private bool FundRandom()
        {
            if (_fundedRandom) return false;
            if (_gameData.Generation < 10) return false;
            return _fundedRandom = AwardController.Instance.FundAny(_playerData);
        }

        private bool FundThermalist()
        {
            if (_fundedThermalist) return false;
            // Must have at least 20 heat to fund this award
            if (!_playerData.HasResource(ResourceType.Heat, 20)) return false;
            return _fundedThermalist = AwardController.Instance.Fund(AwardType.Thermalist, _playerData);
        }
    }
}