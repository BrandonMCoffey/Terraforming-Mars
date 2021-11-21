using System;
using UnityEngine;

namespace Scripts.States
{
    public class PlayerTurnState : State
    {
        public static event Action<bool> PlayerCanAct;

        private int _actionsThisTurn;

        public override void Enter()
        {
            _actionsThisTurn = 0;
            PlayerCanAct?.Invoke(true);

            StandardProjects.OnUseProject += OnStandardProject;
        }

        public override void Tick()
        {
        }

        public override void Exit()
        {
            StandardProjects.OnUseProject -= OnStandardProject;
        }

        private void PerformAction()
        {
            _actionsThisTurn++;
            if (_actionsThisTurn >= 2) {
                PlayerCanAct?.Invoke(false);
            }
        }

        private void OnStandardProject(StandardProjectType type)
        {
            if (_actionsThisTurn >= 2) return;
            Debug.Log("Activate Project: " + type);
            PerformAction();
        }

        private void OnActivatePatent()
        {
        }
    }
}