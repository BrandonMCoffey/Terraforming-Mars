using System.Collections.Generic;
using System.Linq;
using Scripts.Enums;
using UnityEngine;
using UnityEngine.Video;

namespace Scripts.Grid
{
    public class RandomTile : MonoBehaviour
    {
        public static RandomTile Instance;

        private List<HexTile> _tiles;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _tiles = FindObjectsOfType<HexTile>().ToList();
        }

        public HexTile GetRandomTile(TileType type)
        {
            int attempt = 0;
            while (attempt++ < 99) {
                int rand = Random.Range(0, _tiles.Count);
                var tile = _tiles[rand];
                if (tile.Claimed || type == TileType.None) continue;
                if (tile.WaterTile != (type == TileType.Ocean)) continue;
                if (type == TileType.City && tile.HasAdjacentCity) continue;
                return tile;
            }
            return null;
        }
    }
}