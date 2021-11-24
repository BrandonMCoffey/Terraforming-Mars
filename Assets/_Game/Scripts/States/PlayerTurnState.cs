using System;
using Scripts.Mechanics;
using UnityEngine;

namespace Scripts.States
{
    public class PlayerTurnState : State
    {
        [SerializeField] private PlayerToGrid _playerToGrid;

        public static event Action StartTurn;
        public static event Action EndTurn;

        public static event Action<bool> PlayerCanAct;
        private bool _playerCanAct;

        private int _actionsThisTurn;

        public override void Enter()
        {
            _actionsThisTurn = 0;
            StartTurn?.Invoke();
            SetPlayerCanAct(true);

            StandardProjects.OnUseProject += OnStandardProject;

            StateMachine.Input.Confirm += OnEndTurn;
        }

        public override void Tick()
        {
        }

        public override void Exit()
        {
            SetPlayerCanAct(false);
            EndTurn?.Invoke();

            StandardProjects.OnUseProject -= OnStandardProject;
        }

        private void UpdateActionPerformed()
        {
            _actionsThisTurn++;
            if (_actionsThisTurn >= 2) {
                SetPlayerCanAct(false);
            }
        }

        private void SetPlayerCanAct(bool canAct)
        {
            if (_playerCanAct == canAct) return;
            _playerCanAct = canAct;
            PlayerCanAct?.Invoke(canAct);
        }

        private void OnStandardProject(StandardProjectType type)
        {
            if (_actionsThisTurn >= StateMachine.PlayerData.ActionsPerTurn) return;
            if (type == StandardProjectType.SellPatents) {
                var soldPatent = StateMachine.PlayerData.RemoveFirstPatent();
                if (soldPatent == null) return;

                StateMachine.PlayerData.AddResource(ResourceType.Credits, 1);
                StateMachine.PatentCollection.AddToDiscarded(soldPatent);
                UpdateActionPerformed();
                return;
            }
            if (_playerToGrid.OnStandardProject(type)) {
                UpdateActionPerformed();
                Debug.Log("Activate Project: " + type);
            }
        }

        private void OnActivatePatent()
        {
        }

        private void OnEndTurn()
        {
            StateMachine.ChangeState<EnemyTurnState>();
        }
    }
}