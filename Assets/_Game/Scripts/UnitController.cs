using System.Collections.Generic;
using UnityEngine;
using Utility.Buttons;

namespace Scripts
{
    public class UnitController : MonoBehaviour
    {
        [SerializeField] private GridController _gridController = null;
        [SerializeField] private List<Unit> _unitsToPlace = new List<Unit>();

        [Button]
        public void PlaceUnitRandom()
        {
            if (_unitsToPlace.Count == 0) return;
            var unit = _unitsToPlace[Random.Range(0, _unitsToPlace.Count)];
            _gridController.PlaceRandom(unit);
        }
    }
}