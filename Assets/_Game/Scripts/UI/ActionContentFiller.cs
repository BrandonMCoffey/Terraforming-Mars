using System.Collections.Generic;
using Scripts.Data;
using Scripts.Enums;
using Scripts.States;
using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class ActionContentFiller : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData = null;
        [SerializeField] private RectTransform _parent = null;
        [SerializeField] private TextMeshProUGUI _headerText = null;
        [SerializeField] private List<ProjectContent> _standardProjects = new List<ProjectContent>();
        [SerializeField] private PatentContent _patentBasePrefab = null;
        [SerializeField] private bool _debug = false;

        private List<GameObject> _activeContent = new List<GameObject>();
        private ActionCategory? _activeCategory;

        private bool _canInteract;

        private void OnEnable()
        {
            PlayerTurnState.PlayerCanAct += UpdatePlayer;
            _playerData.OnPatentsChanged += UpdateActions;
        }

        private void OnDisable()
        {
            PlayerTurnState.PlayerCanAct -= UpdatePlayer;
            _playerData.OnPatentsChanged -= UpdateActions;
        }

        public void Fill(int index)
        {
            switch (index) {
                case 0:
                    Fill(ActionCategory.StandardProject);
                    _headerText.text = "Projects";
                    break;
                case 1:
                    Fill(ActionCategory.OwnedPatents);
                    _headerText.text = "Patents";
                    break;
                case 2:
                    Fill(ActionCategory.ActivePatents);
                    _headerText.text = "Active Effects";
                    break;
                case 3:
                    Fill(ActionCategory.CompletedPatents);
                    _headerText.text = "Completed";
                    break;
            }
        }

        public void Fill(ActionCategory category)
        {
            if (_activeCategory == category) return;
            if (_debug) Debug.Log("Currently Selected: " + category, gameObject);
            _activeCategory = category;
            foreach (var content in _activeContent) {
                Destroy(content);
            }
            foreach (var project in _standardProjects) {
                project.gameObject.SetActive(category == ActionCategory.StandardProject);
            }
            switch (category) {
                case ActionCategory.StandardProject:
                    foreach (var project in _standardProjects) {
                        project.SetInteractable(_canInteract);
                    }
                    break;
                case ActionCategory.OwnedPatents:
                    foreach (var patent in _playerData.OwnedPatents) {
                        var newContent = Instantiate(_patentBasePrefab, _parent);
                        newContent.Fill(patent);
                        _activeContent.Add(newContent.gameObject);
                    }
                    break;
                case ActionCategory.ActivePatents:
                    foreach (var patent in _playerData.ActivePatents) {
                        var newContent = Instantiate(_patentBasePrefab, _parent);
                        newContent.Fill(patent);
                        _activeContent.Add(newContent.gameObject);
                    }
                    break;
                case ActionCategory.CompletedPatents:
                    foreach (var patent in _playerData.CompletedPatents) {
                        var newContent = Instantiate(_patentBasePrefab, _parent);
                        newContent.Fill(patent);
                        _activeContent.Add(newContent.gameObject);
                    }
                    break;
            }
        }

        private void UpdatePlayer(bool canAct)
        {
            _canInteract = canAct;
            UpdateActions();
        }

        private void UpdateActions()
        {
            var category = _activeCategory;
            if (category == null) return;
            _activeCategory = null;
            Fill(category.Value);
        }
    }
}