using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class GridSlot : MonoBehaviour
    {
        private List<GridObject> _eventListeners;

        private static int[] _rotations = { 0, 90, 180, 270 };

        public void Setup(int x, int y, GameObject tileArt)
        {
            transform.localPosition = new Vector3(x, 0, y);
            var art = Instantiate(tileArt, transform).transform;
            art.localRotation = Quaternion.Euler(0, _rotations[Random.Range(0, _rotations.Length)], 0);
        }

        public void RegisterListener(GridObject listener)
        {
            if (_eventListeners.Contains(listener)) return;
            _eventListeners.Add(listener);
        }

        public void UnRegisterListener(GridObject listener)
        {
            if (!_eventListeners.Contains(listener)) return;
            _eventListeners.Remove(listener);
        }
    }
}