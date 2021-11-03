using System.Collections;
using UnityEngine;

namespace Utility.Animations
{
    public class CameraShake : MonoBehaviour
    {
        private Coroutine _shakeRoutine;
        private Vector3 _originalPosition;

        public void ShakeConstant(float duration, float intensity)
        {
            if (_shakeRoutine != null) {
                transform.localPosition = _originalPosition;
                StopCoroutine(_shakeRoutine);
            }
            _shakeRoutine = StartCoroutine(ConstantShake(duration, intensity));
        }

        public void ShakeBellCurve(float duration, float minIntensity, float maxIntensity)
        {
            if (_shakeRoutine != null) {
                transform.localPosition = _originalPosition;
                StopCoroutine(_shakeRoutine);
            }
            _shakeRoutine = StartCoroutine(BellCurveShake(duration, minIntensity, maxIntensity));
        }

        private IEnumerator ConstantShake(float duration, float intensity)
        {
            _originalPosition = transform.localPosition;
            for (float t = 0; t < duration; t += Time.deltaTime) {
                transform.localPosition = _originalPosition + Random.insideUnitSphere * intensity;
                yield return null;
            }
            transform.localPosition = _originalPosition;
        }

        private IEnumerator BellCurveShake(float duration, float minIntensity, float maxIntensity)
        {
            _originalPosition = transform.localPosition;
            for (float t = 0; t < duration; t += Time.deltaTime) {
                float delta = 1 - Mathf.Abs(1f - 2 * t / duration);
                float intensity = Mathf.Clamp(minIntensity + maxIntensity * delta, minIntensity, maxIntensity);
                transform.localPosition = _originalPosition + Random.insideUnitSphere * intensity;
                yield return null;
            }
            transform.localPosition = _originalPosition;
        }
    }
}