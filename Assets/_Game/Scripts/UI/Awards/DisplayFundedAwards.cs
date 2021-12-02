using Scripts.Data;
using Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Awards
{
    public class DisplayFundedAwards : MonoBehaviour
    {
        [SerializeField] private IconData _iconData;
        [SerializeField] private Image _award1;
        [SerializeField] private Image _award2;
        [SerializeField] private Image _award3;

        private int _fundedAward = 0;

        private void OnEnable()
        {
            AwardController.OnFundAward += AwardFunded;
        }

        private void OnDisable()
        {
            AwardController.OnFundAward -= AwardFunded;
        }

        private void AwardFunded(AwardType type)
        {
            _fundedAward++;
            switch (_fundedAward) {
                case 1:
                    _award1.sprite = GetAwardIcon(type);
                    _award1.color = Color.white;
                    break;
                case 2:
                    _award2.sprite = GetAwardIcon(type);
                    _award2.color = Color.white;
                    break;
                case 3:
                    _award3.sprite = GetAwardIcon(type);
                    _award3.color = Color.white;
                    break;
            }
        }

        private Sprite GetAwardIcon(AwardType type)
        {
            if (_iconData == null) return null;
            switch (type) {
                case AwardType.Landlord:
                    return _iconData.GetScience(ScienceType.City);
                case AwardType.Banker:
                    return _iconData.GetResource(ResourceType.Credits);
                case AwardType.Scientist:
                    return _iconData.GetScience(ScienceType.Atomic);
                case AwardType.Thermalist:
                    return _iconData.GetResource(ResourceType.Heat, true);
                case AwardType.Miner:
                    return _iconData.GetResource(ResourceType.Iron, true);
            }
            return null;
        }
    }
}