using UnityEngine;

namespace Scripts.CardSystem.Cards
{
    public abstract class CardData : ScriptableObject
    {
        public abstract void RenderCard(CardController controller);
    }
}