using System.Collections;
using System.Collections.Generic;
using Scripts.States;
using Scripts.Structs;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class IntroController : MonoBehaviour
    {
        [Header("Game References")]
        [SerializeField] private IndexedGameObject _generation;
        [SerializeField] private IndexedGameObject _projects;
        [SerializeField] private IndexedGameObject _resources;
        [SerializeField] private IndexedGameObject _scoring;
        [SerializeField] private IndexedGameObject _grid;
        [SerializeField] private IndexedGameObject _pauseButton;
        [SerializeField] private IndexedGameObject _planetStatus;
        [SerializeField] private IndexedGameObject _awardsAndMilestones;

        [Header("Blocks")]
        [SerializeField] private List<GameObject> _blocks;

        [Header("Background")]
        [SerializeField] private Image _raycastBlock;
        [SerializeField] private Image _fadeImage;
        [SerializeField] private float _fadeTime = 2.5f;
        [SerializeField] private float _fadeWaitTime = 0.5f;

        private Color _fadeColor;

        private int _currentBlock;
        private bool _active;

        private void Start()
        {
            ResetIntro();
            _fadeColor = _fadeImage.color;
            StartCoroutine(FadeBackground(_fadeTime, _fadeWaitTime, true));
        }

        private void ResetIntro()
        {
            SetBlockActive(0);
            _raycastBlock.gameObject.SetActive(true);
            _active = true;
        }

        private IEnumerator FadeBackground(float fade, float wait = 0, bool disableAll = false)
        {
            _fadeImage.gameObject.SetActive(true);
            SetBackgroundAlpha(1);
            yield return new WaitForSecondsRealtime(wait);

            if (disableAll && _active) EnableAllElements(false);

            for (float t = 0; t < fade; t += Time.deltaTime) {
                float delta = t / fade;
                SetBackgroundAlpha(1 - delta);
                yield return null;
            }
            SetBackgroundAlpha(0);
            _fadeImage.gameObject.SetActive(false);
        }

        private void SetBackgroundAlpha(float alpha)
        {
            _fadeColor.a = Mathf.Clamp01(alpha);
            _fadeImage.color = _fadeColor;
        }

        private void EnableAllElements(bool enable)
        {
            _generation.SetActive(enable);
            _projects.SetActive(enable);
            _resources.SetActive(enable);
            _scoring.SetActive(enable);
            _grid.SetActive(enable);
            _pauseButton.SetActive(enable);
            _planetStatus.SetActive(enable);
            _awardsAndMilestones.SetActive(enable);
        }

        private void EnableIndexedElements(int index, bool enable)
        {
            if (_generation.Index == index) _generation.SetActive(enable);
            if (_projects.Index == index) _projects.SetActive(enable);
            if (_resources.Index == index) _resources.SetActive(enable);
            if (_scoring.Index == index) _scoring.SetActive(enable);
            if (_grid.Index == index) _grid.SetActive(enable);
            if (_pauseButton.Index == index) _pauseButton.SetActive(enable);
            if (_planetStatus.Index == index) _planetStatus.SetActive(enable);
            if (_awardsAndMilestones.Index == index) _awardsAndMilestones.SetActive(enable);
        }

        public void NextBlock()
        {
            SetBlockActive(++_currentBlock);
            EnableIndexedElements(_currentBlock, true);
        }

        private void SetBlockActive(int num)
        {
            foreach (var block in _blocks) {
                block.SetActive(false);
            }
            _currentBlock = num;
            if (num < 0 || num >= _blocks.Count) return;
            _blocks[num].SetActive(true);
        }

        public void FinishIntro()
        {
            SetBlockActive(-1);
            _raycastBlock.gameObject.SetActive(false);
            _active = false;
            EnableAllElements(true);
            SetupState.StartGame();
        }
    }
}