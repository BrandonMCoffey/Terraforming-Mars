using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Grid
{
    public class GridCreator : MonoBehaviour
    {
        private const string SeedKey = "WorldSeed";
        private const string WidthKey = "WorldWidth";
        private const string HeightKey = "WorldHeight";

        [SerializeField] private GridController _gridController = null;
        [SerializeField] private GridSlot _baseGridSlot = null;
        [SerializeField] private ArtCollection _landTiles = null;

        [SerializeField] private int _width = 8;
        [SerializeField] private int _height = 8;

        [SerializeField] private bool _useSeed = true;

        private int _tempWidth;
        private int _tempHeight;
        private int _seed;

        private void Start()
        {
            _tempWidth = _width;
            _tempHeight = _height;
            GenerateGridSlots();
        }

        public void SetGridWidth(int width)
        {
            _tempWidth = width;
        }

        public void SetGridHeight(int height)
        {
            _tempHeight = height;
        }

        public void GenerateGridSlots()
        {
            if (_gridController == null || _baseGridSlot == null) return;
            UpdateSeed();
            _landTiles.Verify();
            Vector3 offset = new Vector3(-_width / 2f + 0.5f, 0, -_height / 2f + 0.5f);
            var grid = new List<List<GridSlot>>(_width);
            for (int x = 0; x < _width; x++) {
                grid.Add(new List<GridSlot>(_height));
                for (int y = 0; y < _height; y++) {
                    var gridObj = Instantiate(_baseGridSlot, _gridController.transform);
                    gridObj.Setup(_gridController, x, y, offset, _landTiles.GetArt());
                    grid[x].Add(gridObj);
                }
            }
            _gridController.SetGrid(grid);
        }

        public void SaveGrid()
        {
            PlayerPrefs.SetInt(SeedKey, _seed);
            PlayerPrefs.SetInt(WidthKey, _width);
            PlayerPrefs.SetInt(HeightKey, _height);
        }

        private void UpdateSeed()
        {
            if (_useSeed) {
                int w = PlayerPrefs.GetInt(WidthKey);
                _width = w > 0 ? w : _tempWidth;

                int h = PlayerPrefs.GetInt(HeightKey);
                _height = h > 0 ? h : _tempHeight;

                _seed = PlayerPrefs.GetInt(SeedKey);
                Random.InitState(_seed);
            } else {
                _width = _tempWidth;
                _height = _tempHeight;
                _seed = Random.Range(0, 100000);
                Random.InitState(_seed);
            }
        }
    }
}