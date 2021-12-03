using Scripts.Data;
using Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Awards
{
    public class MilestoneClaimHelper : MonoBehaviour
    {
        [SerializeField] private MilestoneType _milestoneType = MilestoneType.Terraformer;
        [SerializeField] private Button _claimButton;
        [SerializeField] private Color _canClaimColor = Color.yellow;
        [SerializeField] private MilestoneController _milestoneController;
        [SerializeField] private GameData _gameData;

        private bool _claimed;

        private void OnEnable()
        {
            if (_milestoneController.ClaimedMilestones.Contains(_milestoneType)) {
                _claimButton.interactable = false;
                return;
            }
            _claimButton.GetComponent<Image>().color = MilestoneController.CanClaimAny(_gameData.CurrentPlayer) ? _canClaimColor : Color.white;
            _claimButton.onClick.AddListener(ClaimMilestone);
        }

        private void OnDisable()
        {
            _claimButton.onClick.RemoveListener(ClaimMilestone);
        }

        private void ClaimMilestone()
        {
            if (_claimed) return;
            _claimButton.GetComponent<Image>().color = Color.white;
            bool success = _milestoneController.Claim(_milestoneType);
            if (success) {
                _claimed = true;
                _claimButton.interactable = false;
            }
        }
    }
}