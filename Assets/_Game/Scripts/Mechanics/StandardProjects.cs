using System;
using Scripts.Enums;
using UnityEngine;

namespace Scripts.Mechanics
{
    /* Available Standard Projects
     * 1) Sell patents: You may discard a number of cards from hand to gain the same number of credits.
     * 2) Power plant: For 11 credits you get to increase your energy production 1 step.
     * 3) Asteroid: For 14 credits you get to increase world temperature 1 step.
     * 4) Aquifer: For 18 credits you get to place an ocean tile.
     * 5) Greenery: For 23 credits you get to place a greenery tile.
     * 6) City: For 25 credits you get to place a city tile.
     */
    public static class StandardProjects
    {
        public static event Action<StandardProjectType> OnUseProject = delegate { };

        public static int NumOfProjects = 8;

        public static void InvokeProject(int index) => InvokeProject(GetProject(index));

        public static void InvokeProject(StandardProjectType type)
        {
            OnUseProject?.Invoke(type);
        }

        public static string GetName(int index) => GetName(GetProject(index));

        public static string GetName(StandardProjectType type)
        {
            return type switch {
                StandardProjectType.SellPatents => "Sell Patents",
                StandardProjectType.PowerPlant  => "Power Plant",
                StandardProjectType.Asteroid    => "Asteroid",
                StandardProjectType.Aquifer     => "Aquifer",
                StandardProjectType.Greenery    => "Greenery",
                StandardProjectType.City        => "City",
                StandardProjectType.Plants      => "Plants",
                StandardProjectType.HeatResidue => "Heat Residue",
                _                               => "None"
            };
        }

        public static string GetActionTitle(StandardProjectType type)
        {
            return type switch {
                StandardProjectType.SellPatents => "Sold Patents",
                StandardProjectType.PowerPlant  => "Activated a Power Plant",
                StandardProjectType.Asteroid    => "Witnessed an Asteroid",
                StandardProjectType.Aquifer     => "Built an Aquifer",
                StandardProjectType.Greenery    => "Built a Greenery (Forest)",
                StandardProjectType.City        => "Established a City",
                StandardProjectType.Plants      => "Built a Greenery (Forest)",
                StandardProjectType.HeatResidue => "Caused Global Warming",
                _                               => "None"
            };
        }

        public static int GetCost(int index) => GetCost(GetProject(index));

        public static int GetCost(StandardProjectType type)
        {
            return type switch {
                StandardProjectType.SellPatents => -1,
                StandardProjectType.PowerPlant  => 11,
                StandardProjectType.Asteroid    => 14,
                StandardProjectType.Aquifer     => 18,
                StandardProjectType.Greenery    => 23,
                StandardProjectType.City        => 25,
                StandardProjectType.Plants      => 8,
                StandardProjectType.HeatResidue => 8,
                _                               => 0
            };
        }

        public static ResourceType GetCostType(int index) => GetCostType(GetProject(index));

        public static ResourceType GetCostType(StandardProjectType type)
        {
            return type switch {
                StandardProjectType.PowerPlant  => ResourceType.Credits,
                StandardProjectType.Asteroid    => ResourceType.Credits,
                StandardProjectType.Aquifer     => ResourceType.Credits,
                StandardProjectType.Greenery    => ResourceType.Credits,
                StandardProjectType.City        => ResourceType.Credits,
                StandardProjectType.Plants      => ResourceType.Plant,
                StandardProjectType.HeatResidue => ResourceType.Heat,
                _                               => ResourceType.None
            };
        }


        public static string GetCostReadable(int index) => GetCostReadable(GetProject(index));

        public static string GetCostReadable(StandardProjectType type)
        {
            int cost = GetCost(type);
            if (cost < 1) {
                return "+" + Mathf.Abs(cost);
            }
            return cost.ToString();
        }

        public static StandardProjectType GetProject(int index)
        {
            return index switch {
                0 => StandardProjectType.SellPatents,
                1 => StandardProjectType.PowerPlant,
                2 => StandardProjectType.Asteroid,
                3 => StandardProjectType.Aquifer,
                4 => StandardProjectType.Greenery,
                5 => StandardProjectType.City,
                6 => StandardProjectType.Plants,
                7 => StandardProjectType.HeatResidue,
                _ => throw new System.ComponentModel.InvalidEnumArgumentException()
            };
        }
    }
}