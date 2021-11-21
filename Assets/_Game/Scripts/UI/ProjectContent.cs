using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class ProjectContent : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleText = null;
        [SerializeField] private TextMeshProUGUI _costText = null;

        public void Fill(int index)
        {
            _titleText.text = StandardProjects.GetName(index);
            _costText.text = StandardProjects.GetCostReadable(index);
        }
    }
}