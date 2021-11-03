using UnityEngine;

namespace Scripts
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private int _gridWidth = 8;
        [SerializeField] private int _gridHeight = 8;

        private GridSlot[,] _grid;

        private void Start()
        {
            _grid = GenerateGridSlots(_gridWidth, _gridHeight);
        }

        private static GridSlot[,] GenerateGridSlots(int width, int height)
        {
            var grid = new GridSlot[width, height];
            return grid;
        }
    }
}