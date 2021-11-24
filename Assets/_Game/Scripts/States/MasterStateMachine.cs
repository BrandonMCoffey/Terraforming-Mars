using Input;
using Scripts.Data;
using UnityEngine;
using Utility.StateMachine;

namespace Scripts.States
{
    public class MasterStateMachine : StateMachineBase
    {
        [SerializeField] private InputController _input;
        [SerializeField] private PlayerData _playerData = null;
        [SerializeField] private PlayerData _opponentData = null;
        [SerializeField] private PatentCollection _patentCollection = null;

        public InputController Input => _input;
        public PlayerData PlayerData => _playerData;
        public PlayerData OpponentData => _opponentData;
        public PatentCollection PatentCollection => _patentCollection;

        private void Awake()
        {
            if (Input == null) {
                _input = FindObjectOfType<InputController>();
            }
        }

        private void Start()
        {
            ChangeState<SetupState>();
        }
    }
}