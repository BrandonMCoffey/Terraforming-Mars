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
            _milestonesClaimed = 0;
            ClaimedMilestones = new List<MilestoneType>(_maxMilestones);
        }

        public bool Claim(MilestoneType type, PlayerData player = null)
        {
            // Check available milestones
            if (_milestonesClaimed >= _maxMilestones || ClaimedMilestones.Contains(type)) return false;

            // Check player
            if (player == null) player = _gameData.CurrentPlayer;
            if (!CanClaim(player, type)) return false;

            // Claim
            ClaimedMilestones.Add(type);
            OnClaimMilestone?.Invoke(type, _gameData.CurrentPlayer);
            player.ClaimMilestone(type);
            _milestonesClaimed++;
            AnnouncementController.Instance.Announce(player.PlayerName + " Claimed the " + type + " Milestone", "");
            return true;
        }

        public bool TryClaimAny(PlayerData player)
        {
            if (_milestonesClaimed >= _maxMilestones) return false;
            if (Claim(MilestoneType.Terraformer, player)) return true;
            if (Claim(MilestoneType.Mayor, player)) return true;
            if (Claim(MilestoneType.Gardener, player)) return true;
            if (Claim(MilestoneType.Builder, player)) return true;
            if (Claim(MilestoneType.Planner, player)) return true;
            return false;
        }

        public static bool CanClaimAny(PlayerData player)
        {
            bool available = false;
            available |= CanClaim(player, MilestoneType.Terraformer);
            available |= CanClaim(player, MilestoneType.Mayor);
            available |= CanClaim(player, MilestoneType.Gardener);
            available |= CanClaim(player, MilestoneType.Builder);
            available |= CanClaim(player, MilestoneType.Planner);
            return available;
        }

        public static bool CanClaim(PlayerData player, MilestoneType type)
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