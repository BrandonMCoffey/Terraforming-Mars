using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scripts.Data;
using Scripts.Enums;
using Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Grid
{
    public class HexTile : MonoBehaviour
    {
        [SerializeField] private bool _waterTile;
        [Header("References")]
        [SerializeField] private IconData _icons;
        [Header("Internal References")]
        [SerializeField] private HexTileClickable _tileClickable;
        [SerializeField] private Image _tileImage;
        [SerializeField] private Image _impactImage;
        [SerializeField] private Image _ownerImage;
        [SerializeField] private Image _hoverImage;
        [SerializeField] private GameObject _waterTileImage;
        [SerializeField] private GameObject _bonusHoverVisual;
        [SerializeField] private List<HexTile> _neighbors = new List<HexTile>();

        public bool WaterTile { get; private set; }

        public static event Action<HexTile> OnTileClicked;

        private TileType _tileType = TileType.None;
        private bool _isHovered;

        public bool Claimed => _tileType != TileType.None;
        public bool IsCity => _tileType == TileType.City;
        public bool HasAdjacentCity => _neighbors.Any(neighbor => neighbor.Claimed && neighbor.IsCity);

        #region Unity Functions

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_waterTile != WaterTile) {
                WaterTile = _waterTile;
                _waterTileImage.gameObject.SetActive(_waterTile);
                _ownerImage.gameObject.SetActive(!_waterTile);
            }
        }
#endif

        private void OnEnable()
        {
            if (_tileClickable != null) {
                _tileClickable.OnClicked += OnClick;
                _tileClickable.OnEnterHover += OnMouseHover;
                _tileClickable.OnExitHover += OnMouseExitHover;
            }
            GameController.OnUpdateHover += CheckHover;
        }

        private void OnDisable()
        {
            if (_tileClickable != null) {
                _tileClickable.OnClicked -= OnClick;
                _tileClickable.OnEnterHover -= OnMouseHover;
                _tileClickable.OnExitHover -= OnMouseExitHover;
            }
            GameController.OnUpdateHover -= CheckHover;
        }

        #endregion

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

        public int SetTile(TileType tile, Color owner)
        {
            _tileImage.sprite = _icons.GetTile(tile);
            _tileImage.color = Color.white;
            _ownerImage.color = owner;
            _tileType = tile;
            int waterBonus = _neighbors.Where(neighbor => neighbor.WaterTile && neighbor.Claimed).Sum(neighbor => 2);
            StartCoroutine(PlacementImpact(tile));
            return waterBonus;
        }

        private void OnMouseHover()
        {
            _isHovered = true;
            CheckHover();
        }

        private void OnMouseExitHover()
        {
            _isHovered = false;
            HideHover();
        }

        public void CheckHover()
        {
            var hover = GameController.Instance.TileToPlace;
            bool valid = _isHovered && !Claimed && hover != TileType.None;
            valid &= WaterTile == (hover == TileType.Ocean);
            if (hover == TileType.City) {
                valid &= !HasAdjacentCity;
            }
            if (valid) {
                ShowHover(hover);
            } else {
                HideHover();
            }
        }

        private void ShowHover(TileType tile)
        {
            _hoverImage.enabled = true;
            _hoverImage.sprite = _icons.GetTile(tile);
            foreach (var neighbor in _neighbors.Where(neighbor => neighbor.WaterTile && neighbor.Claimed)) {
                neighbor.ShowHoverBonus(true);
            }
        }

        private void HideHover()
        {
            _hoverImage.sprite = null;
            _hoverImage.enabled = false;
            foreach (var neighbor in _neighbors) {
                neighbor.ShowHoverBonus(false);
            }
        }

        public void ShowHoverBonus(bool show)
        {
            _bonusHoverVisual.SetActive(show);
        }

        private IEnumerator PlacementImpact(TileType tile)
        {
            Color color = Color.white;
            _impactImage.color = color;
            Vector3 scale = _impactImage.transform.localScale;
            _impactImage.sprite = _icons.GetTile(tile);
            for (float t = 0; t < 0.2f; t += Time.deltaTime) {
                float delta = t / 0.2f;
                color.a = 0.8f + (1 - delta) * 0.2f;
                _impactImage.color = color;
                _impactImage.transform.localScale = scale * (1 + delta * 0.4f);
                yield return null;
            }
            for (float t = 0; t < 0.2f; t += Time.deltaTime) {
                float delta = t / 0.2f;
                color.a = (1 - delta) * 0.8f;
                _impactImage.color = color;
                _impactImage.transform.localScale = scale * (1.4f + delta * 0.1f);
                yield return null;
            }
            _impactImage.gameObject.SetActive(false);
        }
    }
}