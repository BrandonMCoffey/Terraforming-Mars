using UnityEngine;

namespace Scripts.CardSystem
{
    public abstract class CardData : ScriptableObject
    {
        public abstract void RenderCard(CardController controller);
    }
}