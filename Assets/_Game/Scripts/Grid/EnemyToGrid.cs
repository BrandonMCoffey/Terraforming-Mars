using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Units;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Grid
{
    public class EnemyToGrid : MonoBehaviour
    {
        [SerializeField] private List<Unit> _enemyUnits = new List<Unit>();

        public static Action<Unit> AddNewEnemy = delegate { };

        public bool CheckGameOver
        {
            get
            {
                _enemyUnits = _enemyUnits.Where(unit => unit != null).ToList();
                return _enemyUnits.Count == 0;
            }
        }

        private void OnEnable()
        {
            AddNewEnemy += UpdateEnemyCount;
        }

        private void OnDisable()
        {
            AddNewEnemy -= UpdateEnemyCount;
        }

        private void UpdateEnemyCount(Unit enemy)
        {
            _enemyUnits.Add(enemy);
        }

        public void Act()
        {
            if (CheckGameOver) return;
            var unit = _enemyUnits[Random.Range(0, _enemyUnits.Count)];
            // TODO: Unit Actions
        }
    }
}