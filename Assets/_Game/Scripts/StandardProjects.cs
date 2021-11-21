using UnityEngine;

namespace Scripts
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
        public static int NumOfProjects = 6;

        public static StandardProjectType GetProject(int index)
        {
            return index switch
            {
                0 => StandardProjectType.SellPatents,
                1 => StandardProjectType.PowerPlant,
                2 => StandardProjectType.Asteroid,
                3 => StandardProjectType.Aquifer,
                4 => StandardProjectType.Greenery,
                5 => StandardProjectType.City,
                _ => throw new System.ComponentModel.InvalidEnumArgumentException()
            };
        }

        public static string GetName(int index) => GetName(GetProject(index));

        public static string GetName(StandardProjectType type)
        {
            return type switch
            {
                StandardProjectType.SellPatents => "Sell Patents",
                StandardProjectType.PowerPlant  => "Power Plant",
                StandardProjectType.Asteroid    => "Asteroid",
                StandardProjectType.Aquifer     => "Aquifer",
                StandardProjectType.Greenery    => "Greenery",
                StandardProjectType.City        => "City",
                _                               => "Null"
            };
        }

        public static int GetCost(int index) => GetCost(GetProject(index));

        public static int GetCost(StandardProjectType type)
        {
            return type switch
            {
                StandardProjectType.SellPatents => -1,
                StandardProjectType.PowerPlant  => 11,
                StandardProjectType.Asteroid    => 14,
                StandardProjectType.Aquifer     => 18,
                StandardProjectType.Greenery    => 23,
                StandardProjectType.City        => 25,
                _                               => 0
            };
        }

        public static string GetCostReadable(int index)
        {
            int cost = GetCost(index);
            if (cost < 1) {
                return "+" + Mathf.Abs(cost);
            }
            return cost.ToString();
        }
    }
}