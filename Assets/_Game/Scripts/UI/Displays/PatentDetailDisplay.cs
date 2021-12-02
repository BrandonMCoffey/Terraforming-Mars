using Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Displays
{
    public class PatentDetailDisplay : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;
        [SerializeField] private TextMeshProUGUI _patentTitle;
        [SerializeField] private TextMeshProUGUI _patentCost;
        [SerializeField] private TextMeshProUGUI _patentRequirements;
        [SerializeField] private TextMeshProUGUI _patentEffects;
        [SerializeField] private TextMeshProUGUI _patentHonor;
        [SerializeField] private GameObject _sellPatentButton;
        [SerializeField] private Button _activatePatentButton;

        public void Display(PatentData patent, bool sell)
        {
            _patentTitle.text = patent.Name;
            _patentCost.text = "Cost: " + patent.Cost;

            _sellPatentButton.SetActive(sell);
            _activatePatentButton.gameObject.SetActive(!sell);

            _patentRequirements.text = patent.GetConstraintsReadable();
            _patentEffects.text = patent.GetEffectsReadable();

            _patentHonor.gameObject.SetActive(patent.Honor > 0);
            _patentHonor.text = "Gain " + patent.Honor + " Honor";

            if (!sell) {
                _activatePatentButton.interactable = patent.CanActivate(_gameData);
            }
        }
    }
}