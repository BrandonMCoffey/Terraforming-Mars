using System;
using Scripts.Enums;

namespace Scripts.Structs
{
    [Serializable]
    public struct PatentConstraint
    {
        public bool Active;
        public PatentConstraintType Type;
        public ComparisonType Comparison;
        public int Amount;
    }
}