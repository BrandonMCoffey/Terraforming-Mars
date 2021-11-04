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

        public void PlaceRandom(Unit unit)
        {
            var slot = GetRandomSlot();
            slot.PlaceObject(unit);
        }

        public GridSlot GetRandomSlot()
        {
            int x = Random.Range(0, _grid.GetLength(0));
            int y = Random.Range(0, _grid.GetLength(1));
            return _grid[x, y];
        }
    }
}