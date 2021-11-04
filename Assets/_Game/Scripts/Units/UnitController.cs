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
            PlacePlayerUnit(1, 6);
            PlacePlayerUnit(2, 6);
            PlacePlayerUnit(1, 5);

            PlaceEnemyUnit(1, 6);
            PlaceEnemyUnit(2, 6);
            PlaceEnemyUnit(1, 5);
        }

        private void PlacePlayerUnit(int x, int y)
        {
            var playerSlot = _gridController.GetSlot(x, y);
            playerSlot.PlaceObject(_playerUnit, true);
        }

        private void PlaceEnemyUnit(int x, int y)
        {
            var enemySlot = _gridController.GetOppositeSlot(x, y);
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