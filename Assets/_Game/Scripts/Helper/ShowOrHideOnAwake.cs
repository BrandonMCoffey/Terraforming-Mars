using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Helper
{
    public class ShowOrHideOnAwake : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _objToShow = new List<GameObject>();
        [SerializeField] private List<GameObject> _objToHide = new List<GameObject>();

        private void Awake()
        {
            foreach (var obj in _objToHide.Where(obj => obj != null)) {
                obj.SetActive(false);
            }
            foreach (var obj in _objToShow.Where(obj => obj != null)) {
                obj.SetActive(true);
            }
        }
    }
}