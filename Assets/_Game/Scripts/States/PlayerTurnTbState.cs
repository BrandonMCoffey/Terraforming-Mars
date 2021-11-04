using System;
using Scripts.Grid;
using Scripts.StateMachine;
using UnityEngine;

namespace Scripts.States
{
    public class PlayerTurnTbState : TbState
    {
        [SerializeField] private MouseToGrid _mouseToGrid = null;

        public static event Action<int> TurnBegin;
        public static event Action TurnEnd;

        private int _playerTurnCount;

        public override void Enter()
        {
            TurnBegin?.Invoke(++_playerTurnCount);

            StateMachine.Input.Confirm += OnConfirm;
            StateMachine.Input.Cancel += OnCancel;

            _mouseToGrid.LockActions(false);
        }

        public override void Tick()
        {
            if (_playerTurnCount >= 5) {
                _playerTurnCount = 0;
                StateMachine.ChangeState<WinTbState>();
            }
        }

        public override void Exit()
        {
            TurnEnd?.Invoke();

            StateMachine.Input.Confirm -= OnConfirm;
            StateMachine.Input.Cancel -= OnCancel;

            _mouseToGrid.LockActions(true);
        }

        private void OnCancel()
        {
            StateMachine.ChangeState<LoseTbState>();
        }

        private void OnConfirm()
        {
            StateMachine.ChangeState<EnemyTurnTbState>();
        }
    }
}