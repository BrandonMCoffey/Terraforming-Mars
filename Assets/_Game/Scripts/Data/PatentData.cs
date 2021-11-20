using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Data
{
    [CreateAssetMenu]
    public class PatentData : ScriptableObject
    {
        public string Name;
        public int Cost;
        public List<ResourceType> Criteria;
        public List<ResourceType> Resources;
        public List<PatentEffect> Effects;
        public List<TileType> TilesToPlace;
        public int Points;
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