using Scripts.States;
using UnityEngine;

namespace Scripts.Helper
{
    public class ShowOnPlayerTurn : MonoBehaviour
    {
        [SerializeField] private GameObject _obj;
        [SerializeField] private bool _showOnTurnStart = true;
        [SerializeField] private bool _hideOnTurnEnd = true;
        [SerializeField] private bool _invert = false;

        private void Start()
        {
            if (_obj == null) _obj = gameObject;
            if (_showOnTurnStart) {
                PlayerTurnState.StartTurn += () => _obj.SetActive(!_invert);
            }
            if (_hideOnTurnEnd) {
                PlayerTurnState.EndTurn += () => _obj.SetActive(_invert);
            }
            _obj.SetActive(!_invert);
        }
    }
}