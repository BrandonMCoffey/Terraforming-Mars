using Scripts.Data;
using Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Awards
{
    public class DisplayClaimedMilestones : MonoBehaviour
    {
        [SerializeField] private IconData _iconData;
        [SerializeField] private Image _milestone1;
        [SerializeField] private Image _owner1;
        [SerializeField] private Image _milestone2;
        [SerializeField] private Image _owner2;
        [SerializeField] private Image _milestone3;
        [SerializeField] private Image _owner3;

        private int _claimedMilestone;

        private void OnEnable()
        {
            MilestoneController.OnClaimMilestone += MilestoneClaimed;
        }

        private void OnDisable()
        {
            MilestoneController.OnClaimMilestone -= MilestoneClaimed;
        }

        private void MilestoneClaimed(MilestoneType type, PlayerData playerData)
        {
            _claimedMilestone++;
            switch (_claimedMilestone) {
                case 1:
                    _milestone1.sprite = GetMilestoneIcon(type);
                    _owner1.gameObject.SetActive(true);
                    _owner1.color = playerData.PlayerColor;
                    break;
                case 2:
                    _milestone2.sprite = GetMilestoneIcon(type);
                    _owner2.gameObject.SetActive(true);
                    _owner2.color = playerData.PlayerColor;
                    break;
                case 3:
                    _milestone3.sprite = GetMilestoneIcon(type);
                    _owner3.gameObject.SetActive(true);
                    _owner3.color = playerData.PlayerColor;
                    break;
            }
        }

        private Sprite GetMilestoneIcon(MilestoneType type)
        {
            if (_iconData == null) return null;
            switch (type) {
                case MilestoneType.Terraformer:
                    return _iconData.ResourceLevelUp;
                case MilestoneType.Mayor:
                    return _iconData.GetScience(ScienceType.City);
                case MilestoneType.Gardener:
                    return _iconData.GetResource(ResourceType.Plant, true);
                case MilestoneType.Builder:
                    return _iconData.GetResource(ResourceType.Iron, true);
                case MilestoneType.Planner:
                    return _iconData.GetResource(ResourceType.Titanium, true);
            }
            return null;
        }
    }
}