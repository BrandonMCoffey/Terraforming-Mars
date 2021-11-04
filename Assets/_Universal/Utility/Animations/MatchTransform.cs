using UnityEngine;

namespace Utility.Animations
{
    public class MatchTransform : MonoBehaviour
    {
        [SerializeField] private Transform _objectToMatch = null;

        private bool _missingTransform;

        private void OnValidate()
        {
            CheckValid();
        }

        private void Update()
        {
            if (_missingTransform) return;

            transform.SetPositionAndRotation(_objectToMatch.position, _objectToMatch.rotation);
        }

        public void SetTransformToMatch(Transform obj)
        {
            _objectToMatch = obj;
            CheckValid();
        }

        private void CheckValid()
        {
            _missingTransform = _objectToMatch == null;
        }
    }
}