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
    }
}