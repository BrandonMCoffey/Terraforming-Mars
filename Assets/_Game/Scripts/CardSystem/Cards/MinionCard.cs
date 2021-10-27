using UnityEngine;

namespace Scripts.CardSystem
{
    [CreateAssetMenu(menuName = "Card Game/Minion Card")]
    public class MinionCard : CardData
    {
        [Header("Card")]
        [SerializeField] private int _powerLevel = 1;
        [SerializeField] private string _minionName = "Minion";
        [SerializeField] private string _specialDescription = "Description";

        public override void RenderCard(CardController controller)
        {
            controller.SetMinion(_powerLevel, _minionName, _specialDescription);
        }
    }
}