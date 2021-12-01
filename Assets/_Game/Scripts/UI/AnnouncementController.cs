using System;
using System.Collections;
using Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility.Buttons;

namespace Scripts.UI
{
    public class AnnouncementController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _waitTime = 0.5f;
        [SerializeField] private AnimationCurve _popupTiming = AnimationCurve.Linear(0, 0, 1, 1);
        [SerializeField] private float _holdTime = 1.5f;
        [SerializeField] private AnimationCurve _closeTiming = AnimationCurve.Linear(0, 1, 1, 0);

        [Header("References")]
        [SerializeField] private GameData _gameData = null;
        [SerializeField] private GameObject _container = null;
        [SerializeField] private RectMask2D _bannerMask = null;
        [SerializeField] private TextMeshProUGUI _bannerTitle = null;
        [SerializeField] private TextMeshProUGUI _bannerSubtitle = null;

        private void Start()
        {
            _container.SetActive(true);
        }

        private void OnEnable()
        {
            if (_gameData == null) return;
            _gameData.Player.OnTurnStart += AnnouncePlayer1;
            _gameData.Opponent.OnTurnStart += AnnouncePlayer2;
        }

        private void OnDisable()
        {
            if (_gameData == null) return;
            _gameData.Player.OnTurnStart -= AnnouncePlayer1;
            _gameData.Opponent.OnTurnStart -= AnnouncePlayer2;
        }

        private void AnnouncePlayer1()
        {
            Announce(_gameData.Player.PlayerName + "'s Turn!", "");
        }

        private void AnnouncePlayer2()
        {
            Announce(_gameData.Opponent.PlayerName + "'s Turn!", "");
        }

        [Button(Spacing = 10)]
        public void Announce(string title, string subtitle)
        {
            _bannerTitle.text = title;
            _bannerSubtitle.text = subtitle;
            // TODO: If another announcement active, wait for it to finish
            StartCoroutine(AnnounceRoutine(_waitTime, _holdTime));
        }

        public void Announce(string title, string subtitle, float waitTime, float holdTime)
        {
            _bannerTitle.text = title;
            _bannerSubtitle.text = subtitle;
            StartCoroutine(AnnounceRoutine(waitTime, holdTime));
        }

        private IEnumerator AnnounceRoutine(float wait, float hold)
        {
            _container.SetActive(true);
            SetBannerHeight(0);
            for (float t = 0; t < wait; t += Time.deltaTime) {
                //float delta = t / wait;
                yield return null;
            }
            float start = _popupTiming.keys[0].time;
            float end = _popupTiming.keys[_popupTiming.length - 1].time;
            for (float t = start; t < end; t += Time.deltaTime) {
                float value = Mathf.Clamp01(_popupTiming.Evaluate(t));
                SetBannerHeight(value);
                yield return null;
            }
            for (float t = 0; t < hold; t += Time.deltaTime) {
                //float delta = t / hold;
                yield return null;
            }
            start = _closeTiming.keys[0].time;
            end = _closeTiming.keys[_closeTiming.length - 1].time;
            for (float t = start; t < end; t += Time.deltaTime) {
                float value = Mathf.Clamp01(_closeTiming.Evaluate(t));
                SetBannerHeight(value);
                yield return null;
            }
            _container.SetActive(false);
        }

        private void SetBannerHeight(float height)
        {
            var padding = _bannerMask.padding;
            padding.w = 125 - height * 125; // Top Padding
            padding.y = 125 - height * 125; // Bottom Padding
            _bannerMask.padding = padding;
        }
    }
}