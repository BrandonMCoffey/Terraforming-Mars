using Input;
using Scripts.Data;
using UnityEngine;
using Utility.StateMachine;

namespace Scripts.States
{
    public class MasterStateMachine : StateMachineBase
    {
        [SerializeField] private InputController _input;
        [SerializeField] private PlayerData _player = null;
        [SerializeField] private PlayerData _opponent = null;
        [SerializeField] private PlanetData _planet = null;
        [SerializeField] private PatentCollection _patentCollection = null;

        public InputController Input => _input;
        public PlayerData Player => _player;
        public PlayerData Opponent => _opponent;
        public PlanetData Planet => _planet;
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