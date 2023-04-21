using System;

namespace Scripts.Structs
{
    [Serializable]
    public struct Resource
    {
        public int Amount;
        public int Level;

        public Resource(int amount)
        {
            Amount = amount;
            Level = 1;
        }
    }
}