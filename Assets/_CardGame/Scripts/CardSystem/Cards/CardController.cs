using TMPro;
using UnityEngine;

namespace Scripts.CardSystem.Cards
{
    public class CardController : MonoBehaviour
    {
        [Header("Header")]
        [SerializeField] private TextMeshProUGUI _actionTitle = null;
        [SerializeField] private TextMeshProUGUI _minionTitle = null;
        [SerializeField] private TextMeshProUGUI _powerLevel = null;

        [Header("Description")]
        [SerializeField] private TextMeshProUGUI _cardType = null;
        [SerializeField] private TextMeshProUGUI _description = null;

        private static string _typeAction = "Action";
        private static string _typeMinion = "Minion";

        public void SetAction(string title, string description)
        {
            EnableUI(_actionTitle, true);
            EnableUI(_minionTitle, false);
            EnableUI(_powerLevel, false);

            _actionTitle.text = title;
            _cardType.text = _typeAction;
            _description.text = description;
        }

        public void SetMinion(int power, string title, string description)
        {
            EnableUI(_actionTitle, false);
            EnableUI(_minionTitle, true);
            EnableUI(_powerLevel, true);

            _powerLevel.text = power.ToString();
            _minionTitle.text = title;
            _cardType.text = _typeMinion;
            _description.text = description;
        }

        public void SetStyle(TMP_FontAsset font, Color textColor)
        {
            _actionTitle.font = font;
            _minionTitle.font = font;
            _powerLevel.font = font;
            _cardType.font = font;
            _description.font = font;

            _actionTitle.color = textColor;
            _minionTitle.color = textColor;
            _powerLevel.color = textColor;
            _cardType.color = textColor;
            _description.color = textColor;
        }

        private static void EnableUI(Component text, bool enabled)
        {
            if (text != null) {
                text.gameObject.SetActive(enabled);
            }
        }
    }
}