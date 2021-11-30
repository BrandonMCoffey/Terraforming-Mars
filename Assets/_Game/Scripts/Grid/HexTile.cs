using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Data;
using Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Grid
{
    public class HexTile : MonoBehaviour
    {
        [SerializeField] private bool _waterTile = false;
        [Header("References")]
        [SerializeField] private IconData _icons = null;
        [Header("Internal References")]
        [SerializeField] private HexTileClickable _tileClickable = null;
        [SerializeField] private Image _tileImage = null;
        [SerializeField] private Image _ownerImage = null;
        [SerializeField] private GameObject _waterTileImage = null;
        [SerializeField] private List<HexTile> _neighbors = new List<HexTile>();

        public bool Claimed { get; private set; }
        public bool WaterTile { get; private set; }

        public static event Action<HexTile> OnTileClicked;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_waterTile != WaterTile) {
                WaterTile = _waterTile;
                _waterTileImage.gameObject.SetActive(_waterTile);
            }
        }
#endif

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

        private void OnClick()
        {
            OnTileClicked?.Invoke(this);
        }

        public void SetTile(TileType tile, Color owner)
        {
            _tileImage.sprite = _icons.GetTile(tile);
            _tileImage.color = Color.white;
            _ownerImage.color = owner;
            Claimed = true;
        }
    }
}