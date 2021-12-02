using System;
using Scripts.Enums;
using UnityEngine;
using Utility.Inspector;

namespace Scripts.Data
{
    [CreateAssetMenu(menuName = "TM/Game Data")]
    public class GameData : ScriptableObject
    {
        [SerializeField] private int _turnsPerGeneration = 2;
        [SerializeField] [ReadOnly] private int _generation = 1;
        [SerializeField] [ReadOnly] private int _generationTurn = 1;
        [SerializeField] private PlayerData _player;
        [SerializeField] private PlayerData _opponent;
        [SerializeField] private PatentCollection _patentCollection;
        [SerializeField] private PlanetType _planetType = PlanetType.Mars;
        [SerializeField] private PlanetData _mars;
        [SerializeField] private PlanetData _moon;

        public event Action<int> OnGenerationChange;
        public PlayerData CurrentPlayer => Player.CurrentTurn ? Player : Opponent;
        public PlayerData OtherPlayer => Player.CurrentTurn ? Opponent : Player;
        public PatentCollection PatentCollection => _patentCollection;
        public int Generation => _generation;


        public PlayerData Player {
            get => _player;
            set => _player = value;
        }

        public PlayerData Opponent {
            get => _opponent;
            set => _opponent = value;
        }

        public PlanetType PlanetType {
            get => _planetType;
            set => _planetType = value;
        }

        public PlanetData Planet {
            get {
                return PlanetType switch {
                    PlanetType.Mars => _mars,
                    PlanetType.Moon => _moon,
                    _               => _mars
                };
            }
        }

        public bool PlayerActive(PlayerTypes type)
        {
            return type switch {
                PlayerTypes.Player1 => true,
                PlayerTypes.Player2 => false,
                PlayerTypes.AnyUser => Player.UserControlled,
                PlayerTypes.AnyAi   => !Player.UserControlled,
                _                   => false
            };
        }

        public bool OpponentActive(PlayerTypes type)
        {
            return type switch {
                PlayerTypes.Player1 => false,
                PlayerTypes.Player2 => true,
                PlayerTypes.AnyUser => Opponent.UserControlled,
                PlayerTypes.AnyAi   => !Opponent.UserControlled,
                _                   => false
            };
        }

        public void SetGeneration(int gen)
        {
            _generation = gen;
            _generationTurn = 1;
            OnGenerationChange?.Invoke(gen);
        }

        public bool IncrementGeneration()
        {
            if (++_generationTurn > _turnsPerGeneration * 2) {
                OnGenerationChange?.Invoke(++_generation);
                _generationTurn = 1;
                return true;
            }
            return false;
        }
    }
}