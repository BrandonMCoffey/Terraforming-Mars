using Scripts.Data;
using TMPro;
using UnityEngine;

namespace Scripts.UI.Displays
{
    public class PatentContent : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _costText;

        public void Fill(PatentData patent)
        {
            _titleText.text = patent.Name;
            _costText.text = patent.Cost.ToString();
        }
    }
}