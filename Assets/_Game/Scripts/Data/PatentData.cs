using System;
using UnityEngine;

namespace Scripts.Data
{
    [CreateAssetMenu]
    public class PatentData : ScriptableObject
    {
        [Header("Patent Info")]
        public string Name;
        public int Cost;
        public int Points;
        [Header("Patent Criteria")]
        public ResourceType Criteria1;
        public ResourceType Criteria2;
        public ResourceType Criteria3;
        [Header("Patent Resources")]
        public ResourceType Resource1;
        public ResourceType Resource2;
        [Header("Patent Effects")]
        public PatentEffect Effect1;
        public PatentEffect Effect2;
        public TileType TileToPlace;
    }

    [Serializable]
    public struct PatentEffect
    {
        public ResourceType Resource;
        public int Amount;
        public bool Level;
        public bool EffectEnemy;
    }
}