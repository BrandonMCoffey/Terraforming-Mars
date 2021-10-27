using UnityEngine;

namespace Scripts.CardSystem
{
    [CreateAssetMenu(menuName = "Card Game/Action Card")]
    public class ActionCard : CardData
    {
        [SerializeField] private int _actionName;
        [SerializeField] private int _actionDescription;
    }
}