using System;
using Scripts.Data;
using UnityEngine;

namespace Scripts.Mechanics
{
    public class PlayerToGrid : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData = null;

        public bool OnStandardProject(StandardProjectType type)
        {
            if (type == StandardProjectType.SellPatents) {
                var soldPatent = _playerData.RemoveFirstPatent();
                if (soldPatent == null) return false;

                _playerData.AddResource(ResourceType.Credits, 1);
                return true;
            }

            int cost = StandardProjects.GetCost(type);
            bool successfullyPaidFor = _playerData.RemoveResource(ResourceType.Credits, cost);

            if (!successfullyPaidFor) return false;

            // Perform Standard Project

            return true;
        }
    }
}