using GridTool.DataScripts;
using Scripts.States;
using UnityEngine;

namespace Scripts.Units
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private UnitData _data = null;
        [SerializeField] private int _movesThisTurn;
        [SerializeField] private int _attacksThisTurn;

        public bool PlayerOwned { get; set; }

        public bool CanMove => _movesThisTurn == 0;

        public bool CanAttack => _attacksThisTurn == 0;

        public UnitData Data => _data;

        private void OnEnable()
        {
            PlayerTurnTbState.TurnBegin += ResetTurn;
        }

        public void AddUnitMoved()
        {
            _movesThisTurn++;
        }

        public void AddUnitAttacked()
        {
            _attacksThisTurn++;
        }

        private void ResetTurn(int ignore)
        {
            _movesThisTurn = 0;
        }
    }
}