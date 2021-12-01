using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Scripts.UI.Helper
{
    public class DebugRaycaster : MonoBehaviour
    {
        [SerializeField] private bool _debug = false;

#if UNITY_EDITOR
        private void Update()
        {
            if (!_debug) return;
            var eventData = new PointerEventData(EventSystem.current) {
                position = Mouse.current.position.ReadValue()
            };
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            string output = results.Aggregate("Hit: ", (current, result) => current + (result.gameObject.name + ", "));
            Debug.Log(output);
        }
#endif
    }
}