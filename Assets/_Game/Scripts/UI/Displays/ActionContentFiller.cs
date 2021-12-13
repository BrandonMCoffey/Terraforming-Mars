using System.Collections.Generic;
using Scripts.Data;
using Scripts.Enums;
using Scripts.States;
using Scripts.UI.Helper;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Displays
{
    public class ActionContentFiller : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;
        [SerializeField] private RectTransform _parent;
        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private List<ProjectContent> _standardProjects = new List<ProjectContent>();
        [SerializeField] private List<PatentContent> _patents = new List<PatentContent>();
        [SerializeField] private PatentContent _patentBasePrefab;
        [SerializeField] private Button _projectsButton;
        [SerializeField] private Button _patentsButton;
        [SerializeField] private bool _debug;

        private bool _projectsActive = true;

        public void ShowProjects()
        {
            _projectsButton.interactable = false;
            _patentsButton.interactable = true;
            foreach (var project in _standardProjects) {
                project.gameObject.SetActive(true);
                project.SetInteractable(_gameData.CurrentPlayer.CanAct);
            }
            foreach (var patent in _patents) {
                patent.gameObject.SetActive(false);
            }
        }

        public void ShowPatents()
        {
            _projectsButton.interactable = true;
            _patentsButton.interactable = false;
            foreach (var project in _standardProjects) {
                project.gameObject.SetActive(false);
            }
            int index = 0;
            foreach (var patent in _gameData.CurrentPlayer.OwnedPatents) {
                var patentContent = GetPatentContent(index++);
                patentContent.Fill(patent, _gameData);
            }
            foreach (var patent in _gameData.CurrentPlayer.CompletedPatents) {
                var patentContent = GetPatentContent(index++);
                patentContent.Fill(patent, _gameData, true);
            }
            for (int i = index; i < _patents.Count; i++) {
                Destroy(_patents[i].gameObject);
            }
            foreach (var patent in _patents) {
                patent.gameObject.SetActive(true);
            }
        }

        private PatentContent GetPatentContent(int index)
        {
            if (index < _patents.Count) return _patents[index];

            var patent = Instantiate(_patentBasePrefab, _parent);
            _patents.Add(patent);
            return patent;
        }

        public void UpdateContent()
        {
            if (_projectsActive) {
                ShowProjects();
            } else {
                ShowPatents();
            }
        }
    }
}