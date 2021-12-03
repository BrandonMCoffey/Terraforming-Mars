using System;
using Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility.Buttons;

namespace Scripts.UI.Displays
{
    public class ResearchDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _patentTitle;
        [SerializeField] private TextMeshProUGUI _patentCost;
        [SerializeField] private TextMeshProUGUI _patentRequirements;
        [SerializeField] private TextMeshProUGUI _patentEffects;
        [SerializeField] private GameObject _patentHonorObj;
        [SerializeField] private TextMeshProUGUI _patentHonor;
        [SerializeField] private Button _button;
        [SerializeField] private Image _borderImage;
        [SerializeField] private Color _completeColor = Color.green;

        private Color _currentColor;

        public event Action<ResearchDisplay> OnPurchase;

        public PatentData PatentData { get; private set; }

        private void Awake()
        {
            _currentColor = _borderImage.color;
        }

        [Button(Spacing = 10)]
        public void Display(PatentData patent)
        {
            _button.interactable = false;
            _borderImage.color = _currentColor;
            PatentData = patent;
            if (patent == null) return;
            _button.interactable = true;

            _patentTitle.text = patent.Name;
            _patentCost.text = patent.Cost1.Amount + " " + patent.Cost1.Resource;
            if (patent.Cost2.Active) {
                _patentCost.text += " or " + patent.Cost2.Amount + " " + patent.Cost2.Resource;
            }

            _patentHonorObj.SetActive(patent.Honor > 0);
            _patentHonor.text = "Gain " + patent.Honor + " Honor";

            _patentRequirements.text = patent.GetConstraintsReadable();
            _patentEffects.text = patent.GetEffectsReadable();
        }

        public void Purchase()
        {
            OnPurchase?.Invoke(this);
        }

        public void Complete()
        {
            _borderImage.color = _completeColor;
            _button.interactable = false;
        }
    }
}