using System;
using Scripts.StateMachine;

namespace Scripts.States
{
    public class LoseTbState : TbState
    {
        public static event Action LoseGame;

        public override void Enter()
        {
            LoseGame?.Invoke();
            StateMachine.Input.Confirm += OnConfirm;
        }

        public override void Exit()
        {
            StateMachine.Input.Confirm -= OnConfirm;
        }

        private void OnConfirm()
        {
            // TODO: Restart game without reloading the scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}