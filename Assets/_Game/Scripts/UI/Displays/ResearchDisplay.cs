using Scripts.Data;
using TMPro;
using UnityEngine;
using Utility.Buttons;

namespace Scripts.UI.Displays
{
    public class ResearchDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _patentTitle;

        [Button(Spacing = 10)]
        public void Display(PatentData patent)
        {
            if (patent == null) return;
            _patentTitle.text = patent.Name;
        }
    }
}