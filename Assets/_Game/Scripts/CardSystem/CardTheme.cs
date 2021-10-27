using TMPro;
using UnityEngine;

namespace Scripts.CardSystem
{
    [CreateAssetMenu(menuName = "Card Game/Card Theme")]
    public class CardTheme : ScriptableObject
    {
        [SerializeField] private TMP_FontAsset _font;
        [SerializeField] private Color _color;
        [SerializeField] private Sprite _icon;
        [SerializeField] private Sprite _lowerBackground;
    }
}