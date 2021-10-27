using EasyButtons;
using UnityEngine;

namespace Scripts.CardSystem
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private CardTheme _theme;
        [SerializeField] private CardData _card;
        [SerializeField] private CardController _cardUI;

        [Button("Render")]
        public void RenderCard(string text, int num, Transform trans)
        {
            Debug.Log("Hello " + text);
        }
    }
}