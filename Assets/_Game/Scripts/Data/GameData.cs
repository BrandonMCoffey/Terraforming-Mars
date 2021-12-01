using Scripts.Enums;
using UnityEngine;

namespace Scripts.Data
{
    [CreateAssetMenu(menuName = "TM/Game Data")]
    public class GameData : ScriptableObject
    {
        [SerializeField] private PlayerData _player;
        [SerializeField] private PlayerData _opponent;
        [SerializeField] private PlanetType _planet = PlanetType.Mars;

        public PlayerData CurrentPlayer => Player.CurrentTurn ? Player : Opponent;

        public PlayerData Player
        {
            get => _player;
            set => _player = value;
        }

        public PlayerData Opponent
        {
            get => _opponent;
            set => _opponent = value;
        }

        public PlanetType Planet
        {
            get => _planet;
            set => _planet = value;
        }

        public bool PlayerActive(PlayerTypes type)
        {
            return type switch
            {
                PlayerTypes.Player1 => true,
                PlayerTypes.Player2 => false,
                PlayerTypes.AnyUser => Player.UserControlled,
                PlayerTypes.AnyAi   => !Player.UserControlled,
                _                   => false
            };
        }

        public bool OpponentActive(PlayerTypes type)
        {
            return type switch
            {
                PlayerTypes.Player1 => false,
                PlayerTypes.Player2 => true,
                PlayerTypes.AnyUser => Opponent.UserControlled,
                PlayerTypes.AnyAi   => !Opponent.UserControlled,
                _                   => false
            };
        }
    }
}