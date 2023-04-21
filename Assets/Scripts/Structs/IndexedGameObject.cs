using System;
using UnityEngine;

namespace Scripts.Structs
{
    [Serializable]
    public struct IndexedGameObject
    {
        public GameObject gameObject;
        public int Index;

        public IndexedGameObject(GameObject obj)
        {
            gameObject = obj;
            Index = 0;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}