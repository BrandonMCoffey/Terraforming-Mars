using UnityEngine;

namespace Utility.Animations
{
    public class FollowTransform : MonoBehaviour
    {
        [SerializeField] private Transform _objectToFollow = null;
        [SerializeField] private float _smoothSpeed = 4;

        private bool _missingTransform;

        private void OnValidate()
        {
            _missingTransform = _objectToFollow == null;
        }

        private void Update()
        {
            if (_missingTransform) return;

            Vector3 desiredPosition = _objectToFollow.position;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
        }
    }
}