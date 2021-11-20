using System;
using Scripts.StateMachine;
using UnityEngine.SceneManagement;

namespace Scripts.States
{
    public class WinTbState : TbState
    {
        public static event Action WinGame;

        public override void Enter()
        {
            WinGame?.Invoke();
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