using System;
using Scripts.Enums;

namespace Scripts.Data.Structs
{
    [Serializable]
    public struct PatentEffect
    {
        public bool Active;
        public PatentEffectType Type;
        public int Amount;
        public ResourceType Resource;
        public TileType Tile;
    }
}