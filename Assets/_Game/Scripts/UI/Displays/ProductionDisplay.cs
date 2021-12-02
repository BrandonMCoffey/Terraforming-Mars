using Scripts.Data;
using Scripts.Enums;
using TMPro;
using UnityEngine;

namespace Scripts.UI.Displays
{
    public class ProductionDisplay : MonoBehaviour
    {
        [SerializeField] private GameData _gameData = null;
        [SerializeField] private bool _isPlayer1 = true;
        [SerializeField] private ResourceType _resourceType = ResourceType.Credits;

        [SerializeField] private TextMeshProUGUI _levelText = null;
        [SerializeField] private TextMeshProUGUI _amountText = null;

        private void OnEnable()
        {
            var player = _isPlayer1 ? _gameData.Player : _gameData.Opponent;
            int resourceLevel = player.GetResource(_resourceType, true);
            _levelText.text = "Level " + resourceLevel;
            if (_resourceType == ResourceType.Credits) {
                resourceLevel += player.Honor;
            } else if (_resourceType == ResourceType.Heat) {
                resourceLevel += player.GetResource(ResourceType.Energy);
            }
            _amountText.text = (resourceLevel < 0 ? "-" : "+") + resourceLevel;
        }
    }
}