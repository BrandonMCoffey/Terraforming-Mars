using UnityEngine;
using Utility.Animations;
using Utility.Pooling;

namespace Scripts.Grid
{
    public class ObjectPoolFollowTransform : ObjectPool
    {
        internal override void PoolNewObject()
        {
            GameObject obj = Instantiate(ObjectToPool, transform);
            obj.AddComponent<MatchTransform>();
            obj.SetActive(false);
            Pool.Add(obj);
        }
    }
}