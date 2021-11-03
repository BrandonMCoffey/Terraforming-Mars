using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.Buttons;

namespace Scripts
{
    public class GridCreator : MonoBehaviour
    {
        [SerializeField] private List<GridSlot> _grid;
        [SerializeField] private GridSlot _baseGridSlot = null;


        [Button]
        private void GenerateGridSlots(int width, int height)
        {
            if (_grid != null) {
                foreach (var gridObj in _grid.Where(gridObj => gridObj != null)) {
                    DestroyImmediate(gridObj.gameObject);
                }
            }
            _grid = new List<GridSlot>(width * height);
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    var gridObj = Instantiate(_baseGridSlot, transform);
                    gridObj.Setup(x, y);
                    _grid.Add(gridObj);
                }
            }
        }
    }
}