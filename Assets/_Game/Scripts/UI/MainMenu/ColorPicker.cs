using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.MainMenu
{
    public class ColorPicker : MonoBehaviour
    {
        [SerializeField] private GameObject _container;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private string _titleTextEnd = "'s Color";
        [SerializeField] private Image _colorDisplay;
        [SerializeField] private Slider _sliderR;
        [SerializeField] private Slider _sliderG;
        [SerializeField] private Slider _sliderB;

        private Color _color;

        private SetPlayerInfo _playerInfo;

        private void Start()
        {
            Close();
        }

        public void Activate(SetPlayerInfo playerInfo)
        {
            _container.SetActive(true);
            _playerInfo = playerInfo;
            _color = playerInfo.GetColor;
            _colorDisplay.color = _color;
            _sliderR.value = _color.r;
            _sliderG.value = _color.g;
            _sliderB.value = _color.b;
            _titleText.text = playerInfo.GetName + _titleTextEnd;
        }

        public void Close()
        {
            _playerInfo = null;
            _container.SetActive(false);
        }

        private void UpdateColor()
        {
            _playerInfo.SetColor(_color);
            _colorDisplay.color = _color;
        }

        public void SetRed(float red)
        {
            _color.r = red;
            UpdateColor();
        }

        public void SetGreen(float green)
        {
            _color.g = green;
            UpdateColor();
        }

        public void SetBlue(float blue)
        {
            _color.b = blue;
            UpdateColor();
        }
    }
}