using Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Awards
{
    public class AwardFundHelper : MonoBehaviour
    {
        [SerializeField] private AwardType _awardType = AwardType.Landlord;
        [SerializeField] private Button _fundButton;
        [SerializeField] private Image _awardBackground;
        [SerializeField] private Color _awardColor = Color.green;
        [SerializeField] private AwardController _awardController;

        private bool _funded;

        private void OnEnable()
        {
            _fundButton.onClick.AddListener(FundAward);
            _fundButton.interactable = !_funded;
        }

        private void OnDisable()
        {
            _fundButton.onClick.RemoveListener(FundAward);
        }

        private void FundAward()
        {
            if (_funded) return;
            bool success = _awardController.Fund(_awardType);
            if (success) {
                _funded = true;
                _fundButton.interactable = false;
                _awardBackground.color = _awardColor;
            }
        }
    }
}