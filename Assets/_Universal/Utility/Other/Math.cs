using UnityEngine;

namespace Utility.Other
{
    public static class CustomMath
    {
        public static float Map(float value, float from1, float to1, float from2, float to2, bool clamp = true)
        {
            if (clamp) value = Mathf.Clamp(value, from1, to1);
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
    }
}