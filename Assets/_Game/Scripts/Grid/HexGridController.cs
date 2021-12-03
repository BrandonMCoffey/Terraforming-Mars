using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utility.Buttons;

namespace Scripts.Grid
{
    public class HexGridController : MonoBehaviour
    {
        [SerializeField] private List<HexTile> _grid = new List<HexTile>();
        [SerializeField] private float _maxDistForNeighbors = 0.5f;

#if UNITY_EDITOR
        [Button(Spacing = 10)]
        private void UpdateNeighbors()
        {
            foreach (var tile in _grid) {
                tile.UpdateNeighbors(_grid, _maxDistForNeighbors);
                EditorUtility.SetDirty(tile);
            }

            EditorUtility.SetDirty(this);
        }

        [Button]
        private void UpdateGrid()
        {
            _grid = new List<HexTile>();
            Traverse(_grid, transform);
        }

        private static void Traverse(ICollection<HexTile> list, Transform obj)
        {
            var temp = obj.GetComponent<HexTile>();
            if (temp != null) list.Add(temp);
            foreach (Transform child in obj) Traverse(list, child);
        }
#endif
    }
}