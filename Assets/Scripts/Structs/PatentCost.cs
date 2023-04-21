using System;
using Scripts.Enums;

namespace Scripts.Structs
{
    [Serializable]
    public struct PatentCost
    {
        public bool Active;
        public ResourceType Resource;
        public int Amount;
    }
}