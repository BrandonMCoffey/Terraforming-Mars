using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.Buttons;

namespace Scripts
{
    public class GridCreator : MonoBehaviour
    {
        [Header("Existing Grid")]
        [SerializeField] private List<GridSlot> _grid;
        [Header("Generate Grid")]
        [SerializeField] private GridSlot _baseGridSlot = null;
        [SerializeField] private List<ArtCollection> _potentialTiles;


        [Button]
        private void GenerateGridSlots(int width, int height)
        {
            if (_baseGridSlot == null) return;
            if (_grid != null) {
                foreach (var gridObj in _grid.Where(gridObj => gridObj != null)) {
                    DestroyImmediate(gridObj.gameObject);
                }
            }
            _potentialTiles = _potentialTiles.Where(obj => obj != null).ToList();
            _grid = new List<GridSlot>(width * height);
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    var gridObj = Instantiate(_baseGridSlot, transform);
                    gridObj.Setup(x, y, _potentialTiles[Random.Range(0, _potentialTiles.Count)].GetTile());
                    _grid.Add(gridObj);
                    gridObj.gameObject.name = _baseGridSlot.name + " (" + (x + 1) + "," + (y + 1) + ")";
                }
            }
        }
    }
}