using Scripts.Data;
using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class PatentContent : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleText = null;

        public void Fill(PatentData patent)
        {
            _titleText.text = patent.Name;
        }
    }
}