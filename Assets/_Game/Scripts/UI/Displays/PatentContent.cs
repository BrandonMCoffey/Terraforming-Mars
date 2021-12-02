using System.Collections.Generic;
using Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Displays
{
    public class PatentContent : MonoBehaviour
    {
        [SerializeField] private IconData _iconData;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _costText;
        [SerializeField] private List<Image> _images;

        public bool _selling = false;

        private PatentData _currentPatent = null;

        public void Fill(PatentData patent, bool sell = false)
        {
            _selling = sell;
            _currentPatent = patent;
            _titleText.text = patent.Name;
            _costText.text = patent.Cost.ToString();

            var icons = patent.GetAltSprites(_iconData);
            foreach (var image in _images) {
                image.enabled = false;
            }
            int index = 0;
            foreach (var icon in icons) {
                _images[index].enabled = true;
                _images[index++].sprite = icon;
            }
            icons = patent.GetEffectSprites(_iconData);
            if (index > 0 && icons.Count > 0) {
                _images[index].enabled = true;
                _images[index++].sprite = _iconData.Spacer;
            }
            foreach (var icon in icons) {
                if (index >= _images.Count) continue;
                _images[index].enabled = true;
                _images[index++].sprite = icon;
            }
        }

        public void OnSelect()
        {
            if (_selling) {
                GameController.Instance.ShowPatentDetails(_currentPatent, true);
            } else {
                GameController.Instance.ShowPatentDetails(_currentPatent, false);
            }
        }
    }
}