using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Data
{
    [CreateAssetMenu]
    public class PlayerPatentData : ScriptableObject
    {
        public List<PatentData> OwnedPatents;
        public List<PatentData> ActivePatents;
        public List<PatentData> CompletedPatents;
    }
}