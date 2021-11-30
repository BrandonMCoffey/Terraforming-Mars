using System;
using Scripts.Mechanics;

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

        public override void Enter()
        {
            _standardProjects ??= new PlayerStandardProjects(StateMachine.Player);
            _actionsThisTurn = 0;
            SetPlayerCanAct(true);
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
            _standardProjects.OnPerformAction -= UpdateActionsPerformed;
            EndTurn?.Invoke();
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
            PlayerCanAct?.Invoke(canAct);
        }

        private void OnEndTurn()
        {
            StateMachine.ChangeState<EnemyTurnState>();
        }
    }
}