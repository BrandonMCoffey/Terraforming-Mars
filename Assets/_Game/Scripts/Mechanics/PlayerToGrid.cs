using System;
using Scripts.Data;
using UnityEngine;

namespace Scripts.Mechanics
{
    public class PlayerToGrid : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;

        public bool OnStandardProject(StandardProjectType type)
        {
            if (type == StandardProjectType.SellPatents) {
                return true;
            }

            int cost = StandardProjects.GetCost(type);
            bool successfullyPaidFor = _playerData.RemoveCredits(cost);

            if (!successfullyPaidFor) return false;

            // Perform Standard Project

            return true;
        }
    }
}