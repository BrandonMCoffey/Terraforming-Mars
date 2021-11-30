using System;
using UnityEngine;

namespace Utility.CustomFloats
{
    [Serializable]
    public struct MinMaxInt
    {
        public int MinValue;
        public int MaxValue;

        public MinMaxInt(int min, int max)
        {
            MinValue = min;
            MaxValue = max;
        }
    }
}