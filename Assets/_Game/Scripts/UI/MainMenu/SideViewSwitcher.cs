using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.MainMenu
{
    public class SideViewSwitcher : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Image _optionsButton;
        [SerializeField] private Image _creditsButton;
        [SerializeField] private Color _selectedColor = Color.yellow;
        [Header("Panels")]
        [SerializeField] private GameObject _planetPanel;
        [SerializeField] private GameObject _optionsPanel;
        [SerializeField] private GameObject _creditsPanel;

        private bool _optionsActive;
        private bool _creditsActive;

        private Color _optionsColor;
        private Color _creditsColor;

        private void Awake()
        {
            _optionsColor = _optionsButton.color;
            _creditsColor = _creditsButton.color;
        }

        private void OnEnable()
        {
            EnablePlanetPanel();
        }

        public void ToggleOptionsPanel()
        {
            EnableOptionsPanel(!_optionsActive);
        }

        public void ToggleCreditsPanel()
        {
            EnableCreditsPanel(!_creditsActive);
        }

        private void EnablePlanetPanel()
        {
            _planetPanel.SetActive(true);
            EnableOptionsPanel(false);
            EnableCreditsPanel(false);
        }

        private void EnableOptionsPanel(bool active)
        {
            if (_creditsActive) {
                EnableCreditsPanel(false);
            }
            _optionsPanel.SetActive(active);
            _optionsActive = active;
            _optionsButton.color = active ? _selectedColor : _optionsColor;
            _planetPanel.SetActive(!active);
        }

        private void EnableCreditsPanel(bool active)
        {
            if (_optionsActive) {
                EnableOptionsPanel(false);
            }
            _creditsPanel.SetActive(active);
            _creditsActive = active;
            _creditsButton.color = active ? _selectedColor : _creditsColor;
            _planetPanel.SetActive(!active);
        }
    }
}