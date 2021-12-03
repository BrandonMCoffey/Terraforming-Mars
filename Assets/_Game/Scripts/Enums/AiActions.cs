using UnityEngine;

namespace Scripts.Enums
{
    public enum AiActions
    {
        None,
        ClaimAnyMilestone,
        FundThermalist,  // Once heat reached and has extra heat
        FundRandomAward, // Only once ever
        PowerPlant,
        Asteroid, // Rare
        Aquifer,  // Often
        Greenery,
        City,
        HeatResidue, // Once over 20 or random
        Plants,      // Once over 20 or random
        FirstPatent
    }
}