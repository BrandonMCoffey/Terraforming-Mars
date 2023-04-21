using Scripts.Data;
using TMPro;
using UnityEngine;

namespace Scripts.UI.Displays
{
    public class DisplayGeneration : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;
        [SerializeField] private TextMeshProUGUI _text;

        private void OnEnable()
        {
            _gameData.OnGenerationChange += UpdateText;
            UpdateText(_gameData.Generation);
        }

        private void OnDisable()
        {
            _gameData.OnGenerationChange += UpdateText;
        }

        private void UpdateText(int generation)
        {
            _text.text = "Generation " + generation;
        }
    }
}