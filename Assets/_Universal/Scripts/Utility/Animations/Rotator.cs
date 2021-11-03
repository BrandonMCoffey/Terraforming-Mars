using UnityEngine;

namespace Scripts.Utility.Animations
{
    public class Rotator : MonoBehaviour
    {
        [Header("Degrees to rotate per second")]
        [SerializeField] private float _rotateSpeed = 360;
        [SerializeField] private Vector3 _rotateDirection = new Vector3(0, 1, 0);

        private Rigidbody _rb;
        private bool _useRb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            if (_rb != null) {
                _useRb = true;
                _rb.useGravity = false;
                _rb.isKinematic = true;
            }
        }

        private void Update()
        {
            if (_useRb) return;
            transform.Rotate(_rotateDirection * _rotateSpeed * Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (!_useRb) return;
            var rotateAmount = _rotateDirection * _rotateSpeed * Time.fixedDeltaTime;
            _rb.MoveRotation(_rb.rotation * Quaternion.Euler(rotateAmount));
        }
    }
}