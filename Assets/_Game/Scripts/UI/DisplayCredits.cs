using Scripts.Data;
using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class DisplayCredits : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private TextMeshProUGUI _text;

        private void Start()
        {
            _playerData.OnCreditsChanged += value => _text.text = value.ToString();
        }
    }
}