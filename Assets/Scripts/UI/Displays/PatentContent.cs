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

        [SerializeField] private Button _buttonButton;
        [SerializeField] private Image _button;
        [SerializeField] private Color _completedColor = Color.green;
        [SerializeField] private Color _cannotPurchaseColor = Color.red;

        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _costText;
        [SerializeField] private TextMeshProUGUI _honorText;

        [SerializeField] private Image _altCostImg;
        [SerializeField] private List<Image> _tags;

        private PatentData _currentPatent;

        public void Fill(PatentData patent, GameData gameData, bool completed = false)
        {
            _currentPatent = patent;
            _titleText.text = patent.Name;
            _costText.text = patent.Cost1.Amount.ToString();

            _altCostImg.gameObject.SetActive(patent.Cost2.Active);
            if (patent.Cost2.Active) {
                _altCostImg.sprite = _iconData.GetResource(patent.Cost2.Resource);
            }

            var tags = patent.GetAltSprites(_iconData);
            for (int i = 0; i < _tags.Count; i++) {
                bool valid = i < tags.Count && tags[i] != null;
                _tags[i].gameObject.SetActive(valid);
                if (valid) _tags[i].sprite = tags[i];
            }

            _honorText.transform.parent.gameObject.SetActive(patent.Honor > 0);
            _honorText.text = patent.Honor.ToString();

            bool canActivate = !completed && patent.CanActivate(gameData);
            _buttonButton.interactable = canActivate;
            if (!canActivate) {
                _button.color = completed ? _completedColor : _cannotPurchaseColor;
            }
        }

        public void OnSelect()
        {
            GameController.Instance.ShowPatentDetails(_currentPatent);
        }
    }
}