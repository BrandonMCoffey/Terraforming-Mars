using System;
using Scripts.Utility.Buttons;
using UnityEngine;

namespace Scripts.CardSystem
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private CardTheme _theme;
        [SerializeField] private CardData _card = null;
        [SerializeField] private CardController _cardUI = null;

        private bool _missingFields;

        private void OnValidate()
        {
            _missingFields = _cardUI == null || _card == null;
        }

        [Button]
        public void RenderCard()
        {
            _card.RenderCard(_cardUI);
        }
    }
}