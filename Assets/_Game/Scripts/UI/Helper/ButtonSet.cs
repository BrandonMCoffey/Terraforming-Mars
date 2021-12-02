using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.UI.Helper
{
    public class ButtonSet : MonoBehaviour
    {
        [SerializeField] private List<Button> _buttons = new List<Button>();
        [SerializeField] private int _onSelectIntOffset;

        public UnityEvent<int> OnSelect = new UnityEvent<int>();

        private int _currentlySelected;

        private void Awake()
        {
            int firstSelected = -1;
            for (var i = 0; i < _buttons.Count; i++) {
                var index = i;
                if (firstSelected < 0 && !_buttons[i].interactable) {
                    firstSelected = index;
                }
                _buttons[i].onClick.AddListener(delegate { SelectItem(index); });
            }
            if (firstSelected < 0) firstSelected = 0;
            _currentlySelected = firstSelected;
        }

        private void Start()
        {
            SelectItem(_currentlySelected);
        }

        private void SelectItem(int index)
        {
            for (var i = 0; i < _buttons.Count; i++) {
                _buttons[i].interactable = i != index;
            }
            OnSelect?.Invoke(index + _onSelectIntOffset);
        }

        public void DeselectAll()
        {
            foreach (var button in _buttons) {
                button.interactable = true;
                _currentlySelected = -1;
            }
        }

        public void ForceSelect(int index)
        {
            for (var i = 0; i < _buttons.Count; i++) {
                _buttons[i].interactable = i != index;
            }
        }
    }
}