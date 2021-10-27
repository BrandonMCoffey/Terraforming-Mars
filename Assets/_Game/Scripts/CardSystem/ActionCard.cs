using UnityEngine;

namespace Scripts.CardSystem
{
    [CreateAssetMenu]
    public class ActionCard : CardData
    {
        [SerializeField] private int _actionName;
        [SerializeField] private int _actionDescription;
    }
}