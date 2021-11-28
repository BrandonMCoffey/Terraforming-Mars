using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Grid
{
    public class HexTile : MonoBehaviour
    {
        [SerializeField] private HexTileClickable _tileClickable;
        [SerializeField] private List<HexTile> _neighbors = new List<HexTile>();

        private void OnEnable()
        {
            if (_tileClickable != null) _tileClickable.OnClicked += OnClick;
        }

        private void OnDisable()
        {
            if (_tileClickable != null) _tileClickable.OnClicked -= OnClick;
        }

        public void UpdateNeighbors(List<HexTile> grid, float maxDist)
        {
            _neighbors = new List<HexTile>();
            foreach (var tile in grid.Where(tile =>
                tile != this && Vector3.Distance(tile.transform.position, transform.position) < maxDist))
                _neighbors.Add(tile);
        }

        public void OnClick()
        {
            SetColor(Color.red);
            foreach (var tile in _neighbors)
                tile.SetColor(Color.yellow);
        }

        public void SetColor(Color color)
        {
            _tileClickable.SetColor(color);
        }
    }
}