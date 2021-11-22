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
                if (_playerData.RemoveFirstPatent() != null) {
                    _playerData.AddCredits(1);
                    return true;
                }
                return false;
            }

            int cost = StandardProjects.GetCost(type);
            bool successfullyPaidFor = _playerData.RemoveCredits(cost);

            if (!successfullyPaidFor) return false;

            // Perform Standard Project

            return true;
        }
    }
}