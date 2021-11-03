using UnityEngine;

namespace Utility.Other
{
    public static class RandomizeArt
    {
        private static int[] _rotations = { 0, 90, 180, 270 };

        public static void RotateRandomClamped(Transform obj)
        {
            obj.localRotation = Quaternion.Euler(0, _rotations[Random.Range(0, _rotations.Length)], 0);
        }
    }
}