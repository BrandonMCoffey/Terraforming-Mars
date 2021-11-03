using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class GridSlot : MonoBehaviour
    {
        private List<GridObject> _eventListeners;

        public void Setup(int x, int y)
        {
            transform.localPosition = new Vector3(x, 0, y);
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