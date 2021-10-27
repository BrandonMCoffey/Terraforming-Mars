using UnityEngine;

namespace Scripts.CardSystem
{
    [CreateAssetMenu(menuName = "Card Game/Minion Card")]
    public class MinionCard : CardData
    {
        [Header("Card")]
        [SerializeField] private int _powerLevel;
        [SerializeField] private string _minionName;
        [SerializeField] private string _specialDescription;
    }
}