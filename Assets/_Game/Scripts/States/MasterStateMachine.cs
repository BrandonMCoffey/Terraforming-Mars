using Input;
using Scripts.Data;
using UnityEngine;
using Utility.Inspector;
using Utility.StateMachine;

namespace Scripts.States
{
    public class MasterStateMachine : StateMachineBase
    {
        [SerializeField] private InputController _input;
        [SerializeField] private GameData _gameData = null;
        [SerializeField] private PlanetData _planet = null;
        [SerializeField] private PatentCollection _patentCollection = null;
        [SerializeField] [ReadOnly] private PlayerData _player;
        [SerializeField] [ReadOnly] private PlayerData _opponent;

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
            _player = _gameData.Player;
            _opponent = _gameData.Opponent;
        }

        private void Start()
        {
            ChangeState<SetupState>();
        }
    }
}