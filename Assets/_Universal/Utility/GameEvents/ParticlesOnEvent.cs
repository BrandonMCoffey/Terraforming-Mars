using UnityEngine;
using Utility.GameEvents.Logic;

namespace Utility.GameEvents
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticlesOnEvent : GameEventListener
    {
        [Tooltip("How many particles to emit when Event is raised.")]
        [SerializeField] private int _particlesToEmit = 10;

        [SerializeField] private Transform _locationToEmit;
        [SerializeField] private Vector3 _transformAdjust = Vector3.zero;

        [SerializeField] private bool _useDirection;
        [SerializeField] private Vector3 _rotationAdjust = Vector3.zero;

        private ParticleSystem _system;

        private void Awake()
        {
            _system = GetComponent<ParticleSystem>();
        }

        public override void OnEventRaised()
        {
            if (_locationToEmit == null) return;
            transform.position = _locationToEmit.position + _transformAdjust;
            if (_useDirection) {
                transform.rotation = _locationToEmit.rotation * Quaternion.Euler(_rotationAdjust);
            }
            _system.Emit(_particlesToEmit);
        }
    }
}