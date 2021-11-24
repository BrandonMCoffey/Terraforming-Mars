using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.Grid
{
    public class HexTile : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private float _alphaThreshold = 0.1f;

        private void Start()
        {
            GetComponent<Image>().alphaHitTestMinimumThreshold = _alphaThreshold;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
        }
    }
}