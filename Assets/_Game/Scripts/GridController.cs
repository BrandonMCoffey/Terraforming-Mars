using UnityEngine;

namespace Scripts
{
    public class GridController : MonoBehaviour
    {
        private GridSlot[,] _grid = new GridSlot[0, 0];

        public void SetGrid(GridSlot[,] newGrid)
        {
            foreach (var gridObj in _grid) {
                if (gridObj == null) continue;
                DestroyImmediate(gridObj.gameObject);
            }
            _grid = newGrid;
        }
    }
}