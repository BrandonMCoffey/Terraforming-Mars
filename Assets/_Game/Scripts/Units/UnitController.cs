using Scripts.Grid;
using UnityEngine;
using Utility.Buttons;

namespace Scripts.Units
{
    public class UnitController : MonoBehaviour
    {
        [SerializeField] private GridController _gridController = null;
        [SerializeField] private Unit _playerUnit = null;
        [SerializeField] private Unit _enemyUnit = null;

        private void Start()
        {
            PlaceStartingUnits();
        }

        public void PlaceStartingUnits()
        {
            var playerSlot = _gridController.GetSlot(1, 1);
            playerSlot.PlaceObject(_playerUnit, true);
            var enemySlot = _gridController.GetOppositeSlot(1, 1);
            enemySlot.PlaceObject(_enemyUnit, false);
        }

        [Button]
        public void PlaceUnitRandom()
        {
            var playerSlot = _gridController.GetRandomSlot();
            playerSlot.PlaceObject(_playerUnit, true);
            var enemySlot = _gridController.GetRandomSlot();
            enemySlot.PlaceObject(_enemyUnit, false);
        }
    }
}