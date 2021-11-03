using System;
using UnityEngine;

namespace Utility.OptionalVariables
{
    [Serializable]
    public struct Optional<T>
    {
        [SerializeField] private bool _enabled;
        [SerializeField] private T _value;

        public Optional(T value)
        {
            _enabled = true;
            _value = value;
        }

        public bool Enabled => _enabled;
        public T Value => _value;
    }
}