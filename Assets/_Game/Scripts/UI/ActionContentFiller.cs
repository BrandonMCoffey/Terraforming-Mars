using System.Collections.Generic;
using Scripts.Data;
using Scripts.Enums;
using Scripts.States;
using UnityEngine;

namespace Scripts.UI
{
    public class ActionContentFiller : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData = null;
        [SerializeField] private RectTransform _parent = null;
        [SerializeField] private List<ProjectContent> _standardProjects = new List<ProjectContent>();
        [SerializeField] private PatentContent _patentBasePrefab = null;

        private List<GameObject> _activeContent = new List<GameObject>();
        private ActionCategory? _activeCategory;

        private bool _canInteract;

        private void Start()
        {
            PlayerTurnState.PlayerCanAct += canAct =>
            {
                _canInteract = canAct;
                UpdateActions();
            };
            _playerData.OnPatentsChanged += UpdateActions;
        }

        public void Fill(int index)
        {
            switch (index) {
                case 0:
                    Fill(ActionCategory.StandardProject);
                    break;
                case 1:
                    Fill(ActionCategory.OwnedPatents);
                    break;
                case 2:
                    Fill(ActionCategory.ActivePatents);
                    break;
                case 3:
                    Fill(ActionCategory.CompletedPatents);
                    break;
            }
        }

        public void Fill(ActionCategory category)
        {
            if (_activeCategory == category) return;
            Debug.Log("Currently Selected: " + category);
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

        private void UpdateActions()
        {
            var category = _activeCategory;
            if (category == null) return;
            _activeCategory = null;
            Fill(category.Value);
        }
    }
}