using GridTool.DataScripts;
using UnityEngine;

namespace GridTool.GameScripts
{
    public class ObjectController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer = null;
        [SerializeField] private ObjectData _object = null;
        [SerializeField] private float _framesPerSecond = 2f;

        private bool _animate;
        private float _animationTime;
        private int _animationFrame;
        private float _timer;

#if UNITY_EDITOR
        // Can be removed later, just here to visualize fps changes in realtime
        private void OnValidate()
        {
            if (_framesPerSecond < 0.1f) {
                _framesPerSecond = 0.1f;
            }
            _animationTime = 1 / _framesPerSecond;
            if (_object != null) {
                _animate = _object.SpriteAnimationFrames > 1;
            }
        }
#endif

        private void OnEnable()
        {
            if (_spriteRenderer == null) {
                _spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            }
            _animationTime = 1 / _framesPerSecond;
            if (_object != null) {
                Reset();
            }
        }

        private void Update()
        {
            if (_animate) {
                _timer += Time.deltaTime;
                if (_timer > _animationTime) {
                    _timer = 0f;
                    _animationFrame++;
                    if (_animationFrame >= _object.SpriteAnimationFrames) {
                        _animationFrame = 0;
                    }
                    SetSprite(_animationFrame);
                }
            }
        }

        public void SetObject(ObjectData objData)
        {
            _object = objData;
            Reset();
        }

        private void Reset()
        {
            _timer = 0;
            _animate = _object.SpriteAnimationFrames > 1;
            _spriteRenderer.color = _object.MixColor;
            SetSprite(_animationFrame);
        }

        private void SetSprite(int frame)
        {
            if (_object == null) return;
            _spriteRenderer.sprite = _object.Static[_animationFrame];
        }
    }

    public enum Direction
    {
        North,
        East,
        South,
        West
    }
}