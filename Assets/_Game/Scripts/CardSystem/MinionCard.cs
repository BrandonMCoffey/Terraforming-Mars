using UnityEngine;

namespace Scripts.CardSystem
{
    [CreateAssetMenu]
    public class MinionCard : CardData
    {
        [Header("Card")]
        [SerializeField] private int _powerLevel;
        [SerializeField] private string _minionName;
        [SerializeField] private string _specialDescription;
    }
}