using UnityEngine;
using Utility.Other;

namespace Scripts
{
    public class GridSlot : MonoBehaviour
    {
        [SerializeField] private Unit _gridUnit;

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

        public void OnSelect()
        {
            HoverSelectedController.instance.SetSelectedParent(transform);
            Debug.Log(_gridUnit);
        }

        public bool PlaceObject(Unit unitToPlace)
        {
            if (_gridUnit != null) return false;

            _gridUnit = Instantiate(unitToPlace, transform);
            RandomizeArt.RotateRandomClamped(unitToPlace.transform);
            return true;
        }
    }
}