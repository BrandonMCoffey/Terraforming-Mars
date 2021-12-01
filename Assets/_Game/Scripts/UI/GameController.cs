using System;
using Scripts.Data;
using Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;

        [SerializeField] private PlanetData _planet = null;
        [SerializeField] private IconData _icons = null;

        [Header("While Placing vs Main")]
        [SerializeField] private GameObject _leftSideMain = null;
        [SerializeField] private GameObject _leftSidePlacing = null;
        [SerializeField] private Image _placingIcon = null;

        public static Action OnCancelPlacingTile;
        public static Action OnUpdateHover;

        public TileType TileToPlace { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void IncreasePlanetStatus(PlanetStatusType type)
        {
            _planet.IncreaseStatus(type);
        }

        public void ShowPlacingTile(TileType tile)
        {
            _leftSideMain.SetActive(false);
            _leftSidePlacing.SetActive(true);
            TileToPlace = tile;
            _placingIcon.sprite = _icons.GetTile(tile);
            OnUpdateHover?.Invoke();
        }

        public void ShowSellPatents()
        {
        }

        public void ShowActions()
        {
            _leftSideMain.SetActive(true);
            _leftSidePlacing.SetActive(false);
            TileToPlace = TileType.None;
            OnUpdateHover?.Invoke();
        }

        public void CancelPlacingTile()
        {
            OnCancelPlacingTile?.Invoke();
        }
    }
}