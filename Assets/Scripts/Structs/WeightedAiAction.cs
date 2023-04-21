using System;
using Scripts.Enums;
using UnityEngine;

namespace Scripts.Structs
{
    // Designned to only be part of a list. No label normally
    [Serializable]
    public struct WeightedAiAction
    {
        [Range(0, 1)] public float Weight;
        public AiActions Action;
    }
}