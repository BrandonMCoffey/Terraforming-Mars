using System;

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