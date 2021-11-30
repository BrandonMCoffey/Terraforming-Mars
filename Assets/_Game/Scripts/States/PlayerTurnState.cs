using System;
using Scripts.Enums;
using Scripts.Mechanics;
using UnityEngine;

namespace Scripts.States
{
    public class PlayerTurnState : State
    {
        public static event Action StartTurn;
        public static event Action EndTurn;

        public static event Action<bool> PlayerCanAct;

        private PlayerStandardProjects _standardProjects;
        private int _actionsThisTurn;
        private bool _playerCanAct;

        private void Start()
        {
            _standardProjects = new PlayerStandardProjects(StateMachine.PlayerData);
        }

        public override void Enter()
        {
            _actionsThisTurn = 0;
            SetPlayerCanAct(true);
            _standardProjects.StartPlayerTurn();
            _standardProjects.OnPerformAction += UpdateActionsPerformed;
            StateMachine.Input.Confirm += OnEndTurn;
            StartTurn?.Invoke();
        }

        public override void Tick()
        {
        }

        public override void Exit()
        {
            SetPlayerCanAct(false);
            _standardProjects.EndPlayerTurn();
            EndTurn?.Invoke();
        }

        private void UpdateActionsPerformed()
        {
            _actionsThisTurn++;
            if (_actionsThisTurn > StateMachine.PlayerData.ActionsPerTurn) {
                PlayerCanAct?.Invoke(false);
            }
        }

        private void SetPlayerCanAct(bool canAct)
        {
            if (_playerCanAct == canAct) return;
            _playerCanAct = canAct;
            PlayerCanAct?.Invoke(canAct);
        }

        private void OnEndTurn()
        {
            StateMachine.ChangeState<EnemyTurnState>();
        }
    }
}