using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Helper
{
    public class AdaptTextColorToBackground : MonoBehaviour
    {
        [SerializeField] private List<TextMeshProUGUI> _texts = new List<TextMeshProUGUI>();
        [SerializeField] private Image _image;

        private void Start()
        {
            if (_image == null) return;
            Color imageColor = _image.color;
            float v = (imageColor.r + imageColor.g + imageColor.b) * 0.333f;
            Color textColor = v > 0.5f ? Color.black : Color.white;
            foreach (var text in _texts) {
                text.color = textColor;
            }
        }
    }
}