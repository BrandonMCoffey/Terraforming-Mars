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
        [SerializeField] private TextMeshProUGUI _honorText;
        [SerializeField] private List<Image> _images;

        private bool _selling = false;

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
                if (index >= 3) continue;
                _images[index].enabled = true;
                _images[index++].sprite = icon;
            }
            icons = patent.GetEffectSprites(_iconData);
            index = 3;
            foreach (var icon in icons) {
                if (index >= 6) continue;
                _images[index].enabled = true;
                _images[index++].sprite = icon;
            }
            _honorText.gameObject.SetActive(patent.Honor > 0);
            _honorText.text = patent.Honor.ToString();
        }

        public void OnSelect()
        {
            GameController.Instance.ShowPatentDetails(_currentPatent, _selling);
        }
    }
}