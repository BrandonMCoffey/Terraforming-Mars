using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private List<GridSlot> _grid = new List<GridSlot>();

        public void SetGrid(List<GridSlot> newGrid)
        {
            foreach (var gridObj in _grid.Where(gridObj => gridObj != null)) {
                DestroyImmediate(gridObj.gameObject);
            }
            _grid = newGrid;
        }
    }
}