using UnityEngine;

namespace Scripts
{
    public class GridSlot : MonoBehaviour
    {
        [SerializeField] private GameObject _gridObject = null;

        private static int[] _rotations = { 0, 90, 180, 270 };

        public void Setup(int x, int y, GameObject tileArt)
        {
            transform.localPosition = new Vector3(x, 0, y);
            if (tileArt != null) {
                var art = Instantiate(tileArt, transform).transform;
                art.localRotation = Quaternion.Euler(0, _rotations[Random.Range(0, _rotations.Length)], 0);
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
            }
        }
    }
}