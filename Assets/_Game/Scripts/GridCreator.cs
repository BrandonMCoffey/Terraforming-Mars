using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.Buttons;

namespace Scripts
{
    public class GridCreator : MonoBehaviour
    {
        [SerializeField] private GridController _gridController = null;
        [SerializeField] private GridSlot _baseGridSlot = null;
        [SerializeField] private ArtCollection _landTiles = null;

        [SerializeField] private int _width = 8;
        [SerializeField] private int _height = 8;

        [Button]
        private void GenerateGridSlots()
        {
            if (_gridController == null || _baseGridSlot == null) return;
            _landTiles.Verify();
            var grid = new GridSlot[_width, _height];
            for (int x = 0; x < _width; x++) {
                for (int y = 0; y < _height; y++) {
                    var gridObj = Instantiate(_baseGridSlot, _gridController.transform);
                    gridObj.Setup(x, y, _landTiles.GetArt());
                    gridObj.gameObject.name = _baseGridSlot.name + " (" + (x + 1) + "," + (y + 1) + ")";
                    grid[x, y] = gridObj;
                }
            }
            _gridController.SetGrid(grid);
        }
    }
}