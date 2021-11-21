using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class ProjectContent : MonoBehaviour
    {
        [SerializeField] private Button _button = null;
        [SerializeField] private TextMeshProUGUI _titleText = null;
        [SerializeField] private TextMeshProUGUI _costText = null;

        public void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void Fill(int index, bool active)
        {
            _titleText.text = StandardProjects.GetName(index);
            _costText.text = StandardProjects.GetCostReadable(index);
            _button.interactable = active;
            if (active) {
                _button.onClick.AddListener(delegate { StandardProjects.InvokeProject(index); });
            }
        }
    }
}