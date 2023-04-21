using Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility.Buttons;

namespace Scripts.UI.MainMenu
{
    public class MenuPlanetSwitcher : MonoBehaviour
    {
        [SerializeField] private MenuPlanetSwitch _menuPlanetSwitch;
        [SerializeField] private PlanetData _currentPlanet;
        [Header("Other PlanetType")]
        [SerializeField] private TextMeshProUGUI _switchPlanetTxt;
        [SerializeField] private TextMeshProUGUI _switchPlanetDesc;
        [SerializeField] private Image _switchPlanetImage;

        private void Start()
        {
            Setup();
        }

        [Button]
        public void Swap()
        {
            _currentPlanet = _menuPlanetSwitch.Swap(_currentPlanet);
            Setup();
        }

        private void Setup()
        {
            _switchPlanetTxt.text = "Switch to " + _currentPlanet.PlanetName;
            _switchPlanetDesc.text = _currentPlanet.PlanetSwitchDescription;
            _switchPlanetImage.sprite = _currentPlanet.PlanetSprite;
        }
    }
}