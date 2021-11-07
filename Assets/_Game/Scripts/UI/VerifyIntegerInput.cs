using GridTool.DataScripts;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.UI
{
    [RequireComponent(typeof(TMP_InputField))]
    public class VerifyIntegerInput : MonoBehaviour
    {
        [SerializeField] private int _defaultValue = 1;
        [SerializeField] private int _minValue = 0;
        [SerializeField] private int _maxValue = 10;
        [SerializeField] [ReadOnly] private int _integerValue;

        public UnityEvent<int> OnValueChanged;

        private TMP_InputField _inputField;


        private void Awake()
        {
            _inputField = GetComponent<TMP_InputField>();
            _integerValue = _defaultValue;
            OnValueChanged?.Invoke(_integerValue);
            VerifyInput();
        }

        public void VerifyInput()
        {
            try {
                int value = int.Parse(_inputField.text);
                _integerValue = Mathf.Clamp(value, _minValue, _maxValue);
                OnValueChanged?.Invoke(_integerValue);
            } catch {
                //Debug.Log(e);
            } finally {
                _inputField.text = _integerValue.ToString();
            }
        }
    }
}