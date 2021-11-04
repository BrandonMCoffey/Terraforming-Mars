using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Grid
{
    public class GridCreator : MonoBehaviour
    {
        [SerializeField] private GridController _gridController = null;
        [SerializeField] private GridSlot _baseGridSlot = null;
        [SerializeField] private ArtCollection _landTiles = null;

        [SerializeField] private int _width = 8;
        [SerializeField] private int _height = 8;

        private void Awake()
        {
            GenerateGridSlots();
        }

        private void GenerateGridSlots()
        {
            if (_gridController == null || _baseGridSlot == null) return;
            _landTiles.Verify();
            var grid = new List<List<GridSlot>>(_width);
            for (int x = 0; x < _width; x++) {
                grid.Add(new List<GridSlot>(_height));
                for (int y = 0; y < _height; y++) {
                    var gridObj = Instantiate(_baseGridSlot, _gridController.transform);
                    gridObj.Setup(_gridController, x, y, _landTiles.GetArt());
                    grid[x].Add(gridObj);
                }
            }
            _gridController.SetGrid(grid);
        }
    }
}