using UnityEngine;

namespace Scripts.States
{
    public class PlayerTurnState : State
    {
        public override void Enter()
        {
            StandardProjects.OnUseProject += OnStandardProject;
        }

        public override void Tick()
        {
        }

        public override void Exit()
        {
            StandardProjects.OnUseProject -= OnStandardProject;
        }

        private void OnStandardProject(StandardProjectType type)
        {
            Debug.Log("Activate Project: " + type);
        }

        private void OnActivatePatent()
        {
        }
    }
}