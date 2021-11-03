using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts
{
    [CreateAssetMenu]
    public class ArtCollection : ScriptableObject
    {
        [SerializeField] private ArtType _type = ArtType.Land;
        [SerializeField] private bool _weighted = true;
        [SerializeField] private List<ArtWeightField> _art;

        public ArtType Type => _type;

        public void Verify()
        {
#if UNITY_EDITOR
            _art = _art.Where(obj => obj.art != null).ToList();
#endif
        }

        public GameObject GetArt()
        {
            return _weighted ? GetRandomWeighted() : GetRandomRaw();
        }

        private GameObject GetRandomRaw()
        {
            if (_art.Count == 0) return null;
            int rand = Random.Range(0, _art.Count);
            return _art[rand].art;
        }

        private GameObject GetRandomWeighted()
        {
            if (_art.Count == 0) return null;
            float sum = TotalWeight();
            float rand = Random.Range(0, sum);
            foreach (var data in _art) {
                if (rand <= data.weight) {
                    return data.art;
                }
                rand -= data.weight;
            }
            return null;
        }

        private float TotalWeight()
        {
            return _art.Sum(data => data.weight);
        }
    }

    [System.Serializable]
    public struct ArtWeightField
    {
        public GameObject art;
        public float weight;
    }

    public enum ArtType
    {
        Land,
        Water,
        Building
    }
}