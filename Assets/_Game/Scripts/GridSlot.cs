using UnityEngine;
using Utility.Other;

namespace Scripts
{
    public class GridSlot : MonoBehaviour
    {
        [SerializeField] private GameObject _gridObject = null;

        public void Setup(int x, int y, GameObject tileArt)
        {
            transform.localPosition = new Vector3(x, 0, y);
            if (tileArt != null) {
                var art = Instantiate(tileArt, transform);
                RandomizeArt.RotateRandomClamped(art.transform);
            } else {
                Debug.LogWarning("No tile art");
            }
        }

        public void OnHover()
        {
            HoverSelectedController.instance.SetHoverParent(transform);
        }

        public void OnSelect(GameObject objToPlace)
        {
            HoverSelectedController.instance.SetSelectedParent(transform);
            if (_gridObject == null) {
                _gridObject = Instantiate(objToPlace, transform);
                RandomizeArt.RotateRandomClamped(_gridObject.transform);
            }
        }
    }
}