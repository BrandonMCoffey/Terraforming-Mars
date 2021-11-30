using Scripts.Data;
using Scripts.Enums;
using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class DisplayResource : MonoBehaviour
    {
        [SerializeField] private ResourceType _type = ResourceType.Credits;
        [SerializeField] private PlayerData _playerData = null;
        [SerializeField] private TextMeshProUGUI _text = null;

        private void Start()
        {
            if (_playerData == null || _text == null) return;
            _playerData.OnResourcesChanged += () => _text.text = _playerData.GetResource(_type).ToString();
        }
    }
}