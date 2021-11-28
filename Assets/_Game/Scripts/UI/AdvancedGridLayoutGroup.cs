using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI {
    public class AdvancedGridLayoutGroup : GridLayoutGroup {
        [SerializeField] private int _cellsPerLine = 4;
        [SerializeField] private float _aspectRatio = 1;

        [SerializeField] private bool _useDynamicPadding;
        [SerializeField] private Vector2Int _normalPaddingLr;
        [SerializeField] private Vector2Int _normalPaddingTb;
        [SerializeField] private Vector2 _dynamicPaddingLr;
        [SerializeField] private Vector2 _dynamicPaddingTb;

        [SerializeField] private bool _useDynamicSpacing;
        [SerializeField] private Vector2 _normalSpacing;
        [SerializeField] private Vector2 _dynamicSpacing;

        public override void SetLayoutVertical()
        {
            if (_cellsPerLine < 1) _cellsPerLine = 1;
            if (_useDynamicPadding) {
                var left = Mathf.FloorToInt(_dynamicPaddingLr.x * cellSize.x / 100);
                var right = Mathf.FloorToInt(_dynamicPaddingLr.y * cellSize.x / 100);
                var top = Mathf.FloorToInt(_dynamicPaddingTb.x * cellSize.y / 100);
                var bottom = Mathf.FloorToInt(_dynamicPaddingTb.y * cellSize.y / 100);
                padding = new RectOffset(left, right, top, bottom);
            }
            else {
                padding = new RectOffset(_normalPaddingLr.x, _normalPaddingLr.y, _normalPaddingTb.x,
                    _normalPaddingTb.y);
            }

            if (_useDynamicSpacing)
                spacing = _dynamicSpacing * (startAxis == Axis.Horizontal ? cellSize.x : cellSize.y) / 100;
            else
                spacing = _normalSpacing;

            var width = GetComponent<RectTransform>().rect.width;
            var useableWidth = width - padding.horizontal - (_cellsPerLine - 1) * spacing.x;
            var cellWidth = useableWidth / _cellsPerLine;
            cellSize = new Vector2(cellWidth, cellWidth * _aspectRatio);

            base.SetLayoutVertical();
        }
    }
}