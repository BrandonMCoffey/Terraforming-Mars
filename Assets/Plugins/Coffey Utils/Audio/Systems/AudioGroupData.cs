using UnityEngine;
using Utility.CustomFloats;

namespace Utility.Audio.Systems
{
    public class AudioGroupData : MonoBehaviour
    {
        [SerializeField] [MinMaxRange(1, 2)] private RangedFloat _minMaxVolume = new RangedFloat(0, 1);
    }
}