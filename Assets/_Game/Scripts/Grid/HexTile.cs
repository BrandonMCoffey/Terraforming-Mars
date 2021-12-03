using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scripts.Data;
using Scripts.Enums;
using Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Grid
{
    public class HexTile : MonoBehaviour
    {
        [SerializeField] private bool _waterTile;
        [Header("References")]
        [SerializeField] private IconData _icons;
        [SerializeField] private SoundData _sounds;
        [Header("Internal References")]
        [SerializeField] private HexTileClickable _tileClickable;
        [SerializeField] private Image _tileImage;
        [SerializeField] private Image _impactImage;
        [SerializeField] private Image _ownerImage;
        [SerializeField] private Image _hoverImage;
        [SerializeField] private GameObject _waterTileImage;
        [SerializeField] private GameObject _bonusHoverObj;
        [SerializeField] private TextMeshProUGUI _bonusHoverVisual;
        [SerializeField] private List<HexTile> _neighbors = new List<HexTile>();

        private PlayerData _owner;

        public bool WaterTile { get; private set; }

        public static event Action<HexTile> OnTileClicked;

        private TileType _tileType = TileType.None;
        private bool _isHovered;

        public bool Claimed => _tileType != TileType.None;
        public bool IsCity => _tileType == TileType.City;
        public bool IsForest => _tileType == TileType.Forest;
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

        private void Start()
        {
            WaterTile = _waterTile;
        }

        #endregion

        public int GetNeighbors(TileType type)
        {
            return _neighbors.Count(tile => tile._tileType == type);
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

        public int SetTile(TileType tile, PlayerData owner)
        {
            _owner = owner;
            _tileImage.sprite = _icons.GetTile(tile);
            _tileImage.color = Color.white;
            _ownerImage.color = owner.PlayerColor;
            _tileType = tile;
            int waterBonus = _neighbors.Where(neighbor => neighbor.WaterTile && neighbor.Claimed).Sum(neighbor => 2);
            int commerceBonus;
            if (tile == TileType.CommercialDistrict) {
                commerceBonus = _neighbors.Where(neighbor => neighbor._tileType == TileType.City).Sum(neighbor => 4);
            } else {
                commerceBonus = _neighbors.Where(neighbor => neighbor._tileType == TileType.CommercialDistrict).Sum(neighbor => 1);
                if (tile == TileType.City) {
                    commerceBonus *= 4;
                }
            }
            StartCoroutine(PlacementImpact(tile));
            switch (tile) {
                case TileType.Ocean:
                    _sounds.AquiferSfx.Play();
                    break;
                case TileType.Forest:
                    _sounds.ForestSfx.Play();
                    break;
                case TileType.City:
                    _sounds.CitySfx.Play();
                    break;
            }
            return waterBonus + commerceBonus;
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
            foreach (var neighbor in _neighbors) {
                if (neighbor.WaterTile && neighbor.Claimed) {
                    neighbor.ShowHoverBonus(2);
                }
                if (neighbor._tileType == TileType.CommercialDistrict) {
                    neighbor.ShowHoverBonus(tile == TileType.City ? 4 : 1);
                }
            }
        }

        private void HideHover()
        {
            _hoverImage.sprite = null;
            _hoverImage.enabled = false;
            foreach (var neighbor in _neighbors) {
                neighbor.ShowHoverBonus(-1);
            }
        }

        public void ShowHoverBonus(int amount)
        {
            _bonusHoverObj.SetActive(amount > 0);
            if (amount > 0) {
                _bonusHoverVisual.text = "+" + amount;
            }
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