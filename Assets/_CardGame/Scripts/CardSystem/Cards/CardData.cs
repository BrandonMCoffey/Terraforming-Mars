using UnityEngine;

namespace _CardGame.Scripts.CardSystem.Cards
{
    public abstract class CardData : ScriptableObject
    {
        public abstract void RenderCard(CardController controller);
    }
}