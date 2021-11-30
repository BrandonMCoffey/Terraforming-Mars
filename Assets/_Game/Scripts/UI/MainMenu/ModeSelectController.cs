using Scripts.Enums;
using UnityEngine;

namespace Scripts.UI.MainMenu
{
    public class ModeSelectController : MonoBehaviour
    {
        [SerializeField] private bool _debug = false;

        private int _hotseatPlayers = 2;
        private AiDifficultyLevels _aiDifficulty = AiDifficultyLevels.Easy;

        public void StartSoloGame()
        {
            Log("Solo Game Started");
        }

        public void SetAiDifficulty(int difficulty)
        {
            _aiDifficulty = difficulty switch
            {
                0 => AiDifficultyLevels.Easy,
                1 => AiDifficultyLevels.Medium,
                2 => AiDifficultyLevels.Hard,
                _ => _aiDifficulty
            };
            Log("AI Difficulty set to " + _aiDifficulty);
        }

        public void StartGameVsAi()
        {
            Log("AI Game Started");
        }

        public void SetHotseatPlayers(int players)
        {
            _hotseatPlayers = players;
            Log(players + " Hotseat Players");
        }

        public void StartHotseatGame()
        {
            Log("Hotseat Game Started");
        }

        private void Log(string message)
        {
            if (_debug) Debug.Log(message, gameObject);
        }
    }
}