using System;
using Scripts.Data;
using Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class CanvasController : MonoBehaviour
    {
        [SerializeField] private IconData _icons = null;

        [Header("While Placing vs Main")]
        [SerializeField] private GameObject _leftSideMain = null;
        [SerializeField] private GameObject _leftSidePlacing = null;
        [SerializeField] private Image _placingIcon = null;

        public static CanvasController Instance;

        public static Action OnCancelPlacingTile;

        private void Awake()
        {
            Instance = this;
        }

        public void ShowPlacingTile(TileType tile)
        {
            _leftSideMain.SetActive(false);
            _leftSidePlacing.SetActive(true);
            _placingIcon.sprite = _icons.GetTile(tile);
        }

        public void ShowSellPatents()
        {
        }

        public void ShowActions()
        {
            _leftSideMain.SetActive(true);
            _leftSidePlacing.SetActive(false);
        }

        public void CancelPlacingTile()
        {
            OnCancelPlacingTile?.Invoke();
        }
    }
}