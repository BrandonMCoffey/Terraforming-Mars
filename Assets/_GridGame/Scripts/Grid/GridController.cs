using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Grid
{
    public class GridController : MonoBehaviour
    {
        private List<List<GridSlot>> _grid = new List<List<GridSlot>>();

        public void SetGrid(List<List<GridSlot>> newGrid)
        {
            foreach (var gridObj in from gridRow in _grid from gridObj in gridRow where gridObj != null select gridObj) {
                DestroyImmediate(gridObj.gameObject);
            }
            _grid = newGrid;
        }

        public void ClearSelection()
        {
            foreach (var gridObj in from gridRow in _grid from gridObj in gridRow where gridObj != null select gridObj) {
                gridObj.ReadyAction(null, false);
            }
        }

        public GridSlot GetSlot(int x, int y)
        {
            if (_grid.Count == 0 || x < 0 || y < 0 || x >= _grid.Count || y >= _grid[0].Count) return null;
            return _grid[x][y];
        }

        public GridSlot GetOppositeSlot(int x, int y)
        {
            int width = _grid.Count;
            int height = _grid[0].Count;
            x = width - x - 1;
            y = height - y - 1;
            if (_grid.Count == 0 || x < 0 || y < 0 || x >= width || y >= height) return null;
            return _grid[x][y];
        }

        public GridSlot GetRandomSlot()
        {
            if (_grid.Count == 0) return null;
            int x = Random.Range(0, _grid.Count);
            int y = Random.Range(0, _grid[0].Count);
            return _grid[x][y];
        }
    }
}