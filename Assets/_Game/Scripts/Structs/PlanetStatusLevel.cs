using System;

namespace Scripts.Structs
{
    [Serializable]
    public struct PlanetStatusLevel
    {
        public int MinValue;
        public int MaxValue;
        public int StepValue;

        public PlanetStatusLevel(int min, int max, int step)
        {
            MinValue = min;
            MaxValue = max;
            StepValue = step;
        }
    }
}