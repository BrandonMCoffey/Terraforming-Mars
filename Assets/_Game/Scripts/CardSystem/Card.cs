using Scripts.Utility.Buttons;
using UnityEngine;

namespace Scripts.CardSystem
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private CardTheme _theme;
        [SerializeField] private CardData _card;
        [SerializeField] private CardController _cardUI;

        [Button]
        public void RenderCard()
        {
        }
    }
}