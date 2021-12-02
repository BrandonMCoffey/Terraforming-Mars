using Scripts.Data;
using Scripts.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.UI.MainMenu
{
    public class ModeSelectController : MonoBehaviour
    {
        private const int MarsPlanetSceneIndex = 1;
        private const int MoonPlanetSceneIndex = 2;

        [SerializeField] private bool _debug;
        [SerializeField] private GameData _gameData;
        [Header("Solo Player")]
        [SerializeField] private PlayerData _mainPlayer;
        [Header("AI Players")]
        [SerializeField] private PlayerData _aiEasyPlayer;
        [SerializeField] private PlayerData _aiMediumPlayer;
        [SerializeField] private PlayerData _aiHardPlayer;
        [SerializeField] private Image _aiColor;
        [Header("Hotseat Players")]
        [SerializeField] private PlayerData _hotseatPlayer1;
        [SerializeField] private PlayerData _hotseatPlayer2;

        private AiDifficultyLevels _aiDifficulty = AiDifficultyLevels.Easy;

        private void Start()
        {
            UpdateAiColor();
        }

        private void StartGame()
        {
            int gameScene = _gameData.PlanetType switch {
                PlanetType.Mars => MarsPlanetSceneIndex,
                PlanetType.Moon => MoonPlanetSceneIndex,
                _               => 0
            };
            Debug.Log("Starting Game on " + _gameData.PlanetType + " at build scene index " + gameScene, gameObject);
            SceneManager.LoadScene(gameScene);
        }

        public void StartSoloGame()
        {
            Log("Solo Game Started");
            _gameData.Player = _mainPlayer;
            _gameData.Opponent = null;
            StartGame();
        }

        public void SetAiDifficulty(int difficulty)
        {
            _aiDifficulty = difficulty switch {
                0 => AiDifficultyLevels.Easy,
                1 => AiDifficultyLevels.Medium,
                2 => AiDifficultyLevels.Hard,
                _ => _aiDifficulty
            };
            UpdateAiColor();
            Log("AI Difficulty set to " + _aiDifficulty);
        }

        public void StartGameVsAi()
        {
            Log("AI Game Started");
            _gameData.Player = _mainPlayer;
            _gameData.Opponent = _aiDifficulty switch {
                AiDifficultyLevels.Easy   => _aiEasyPlayer,
                AiDifficultyLevels.Medium => _aiMediumPlayer,
                AiDifficultyLevels.Hard   => _aiHardPlayer,
                _                         => _aiEasyPlayer
            };
            _gameData.Opponent.PlayerName = _gameData.Opponent.DefaultName;
            _gameData.Opponent.PlayerColor = _gameData.Opponent.DefaultColor;
            StartGame();
        }

        public void StartHotseatGame()
        {
            Log("Hotseat Game Started");
            _gameData.Player = _hotseatPlayer1;
            _gameData.Opponent = _hotseatPlayer2;
            StartGame();
        }

        private void Log(string message)
        {
            if (_debug) Debug.Log(message, gameObject);
        }

        private void UpdateAiColor()
        {
            _aiColor.color = _aiDifficulty switch {
                AiDifficultyLevels.Easy   => _aiEasyPlayer.DefaultColor,
                AiDifficultyLevels.Medium => _aiMediumPlayer.DefaultColor,
                AiDifficultyLevels.Hard   => _aiHardPlayer.DefaultColor,
                _                         => Color.white
            };
        }
    }
}