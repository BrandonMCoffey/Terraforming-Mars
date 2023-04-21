using System;
using Scripts.Enums;

namespace Scripts.Structs
{
    [Serializable]
    public struct PatentEffect
    {
        public bool Active;
        public PatentEffectType Type;
        public int Amount;
        public ResourceType Resource;
        public TileType Tile;
        public PlanetStatusType Status;
    }
}