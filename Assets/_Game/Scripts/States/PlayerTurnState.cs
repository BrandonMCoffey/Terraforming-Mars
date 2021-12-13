using System;
using Scripts.Data;
using Scripts.Mechanics;
using Scripts.UI;
using UnityEngine;

namespace Scripts.States
{
    public class PlayerTurnState : State
    {
        public bool CanEndTurn { get; private set; }

        private PlayerStandardProjects _standardProjects;
        private int _actionsThisTurn;
        private bool _playerCanAct;
        private float _canEndTurnTime;

        private PlayerData _playerData;

        public override void Enter()
        {
            _canEndTurnTime = Time.time + 3f;
            _standardProjects ??= new PlayerStandardProjects(_playerData);
            _actionsThisTurn = 0;
            SetPlayerCanAct(true);
            CanEndTurn = false;
            _standardProjects.OnPerformAction += UpdateActionsPerformed;
            StateMachine.Input.Confirm += OnEndTurn;
            StateMachine.Input.Cancel += OnPause;
            _playerData.StartTurn();
        }

        public override void Tick()
        {
            if (!CanEndTurn && Time.time > _canEndTurnTime) {
                CanEndTurn = true;
            }
        }

        public override void Exit()
        {
            SetPlayerCanAct(false);
            _standardProjects.OnPerformAction -= UpdateActionsPerformed;
            StateMachine.Input.Confirm -= OnEndTurn;
            StateMachine.Input.Cancel -= OnPause;
            _playerData.EndTurn();
        }

        public void Setup(PlayerData playerData)
        {
            _playerData = playerData;
        }

        private void UpdateActionsPerformed()
        {
            _actionsThisTurn++;
            //if (_actionsThisTurn >= StateMachine.Player.ActionsPerTurn) {
            //SetPlayerCanAct(false);
            //}
        }

        private void SetPlayerCanAct(bool canAct)
        {
            if (_playerCanAct == canAct) return;
            _standardProjects.PlayerCanAct(canAct);
            _playerCanAct = canAct;
            _playerData.CanAct = canAct;
        }

        private void OnEndTurn()
        {
            if (CanEndTurn) {
                StateMachine.NextTurn();
            }
        }

        private static void OnPause()
        {
            PauseMenuController.Instance.Pause();
        }
    }
}