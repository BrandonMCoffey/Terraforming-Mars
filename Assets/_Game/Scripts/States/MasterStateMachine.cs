using System.Collections.Generic;
using System.Linq;
using Scripts.Data;
using UnityEngine;
using UserInput;
using Utility.Inspector;
using Utility.StateMachine;

namespace Scripts.States
{
    public class MasterStateMachine : StateMachineBase
    {
        [SerializeField] private InputController _input;
        [SerializeField] private GameData _gameData;
        [SerializeField] private PlanetData _planet;
        [SerializeField] private PatentCollection _patentCollection;
        [SerializeField] [ReadOnly] private PlayerData _player;
        [SerializeField] [ReadOnly] private PlayerData _opponent;

        public InputController Input => _input;
        public PlayerData Player => _player;
        public PlayerData Opponent => _opponent;
        public PlanetData Planet => _planet;
        public PatentCollection PatentCollection => _patentCollection;

        private List<State> _turnStates;
        private int _currentTurn;

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

        public void SetupTurns(List<State> turns)
        {
            _currentTurn = -1;
            _turnStates = turns.Where(turn => turn != null).ToList();
        }

        public void NextTurn()
        {
            _currentTurn++;
            if (_currentTurn >= _turnStates.Count) {
                _currentTurn = 0;
            }
            var turn = _turnStates[_currentTurn];
            ChangeState(turn);
        }
    }
}