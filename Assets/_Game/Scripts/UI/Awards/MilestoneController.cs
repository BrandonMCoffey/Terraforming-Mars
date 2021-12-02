using Scripts.Data;
using Scripts.Enums;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.UI.Awards
{
    public class MilestoneController : MonoBehaviour
    {
        public static MilestoneController Instance;

        [SerializeField] private int _maxMilestones = 3;
        [SerializeField] private GameData _gameData;

        public static event Action<MilestoneType, PlayerData> OnClaimMilestone;

        public List<MilestoneType> ClaimedMilestones { get; private set; }

        private int _milestonesClaimed;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _milestonesClaimed = 0;
            ClaimedMilestones = new List<MilestoneType>(_maxMilestones);
        }

        public bool Claim(MilestoneType type)
        {
            if (_milestonesClaimed >= _maxMilestones || ClaimedMilestones.Contains(type)) return false;
            if (!CanClaimMilestone(_gameData.CurrentPlayer, type)) return false;
            ClaimedMilestones.Add(type);
            OnClaimMilestone?.Invoke(type, _gameData.CurrentPlayer);
            _gameData.CurrentPlayer.ClaimMilestone(type);
            _milestonesClaimed++;
            return true;
        }

        public static bool CanClaimAnyMilestone(PlayerData player)
        {
            bool available = false;
            available |= CanClaimMilestone(player, MilestoneType.Terraformer);
            available |= CanClaimMilestone(player, MilestoneType.Mayor);
            available |= CanClaimMilestone(player, MilestoneType.Gardener);
            available |= CanClaimMilestone(player, MilestoneType.Builder);
            available |= CanClaimMilestone(player, MilestoneType.Planner);
            return available;
        }

        public static bool CanClaimMilestone(PlayerData player, MilestoneType type)
        {
            if (Instance != null && Instance.ClaimedMilestones.Contains(type)) return false;
            return type switch {
                MilestoneType.Terraformer => player.Honor >= 35,
                MilestoneType.Mayor       => player.OwnedCities.Count >= 3,
                MilestoneType.Gardener    => player.OwnedForests >= 3,
                MilestoneType.Builder     => player.PlacedTiles >= 8,
                MilestoneType.Planner     => player.OwnedPatents.Count >= 16,
                _                         => false
            };
        }
    }
}