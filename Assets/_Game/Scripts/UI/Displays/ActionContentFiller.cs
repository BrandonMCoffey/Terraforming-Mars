using System.Collections.Generic;
using Scripts.Data;
using Scripts.Enums;
using Scripts.States;
using Scripts.UI.Helper;
using TMPro;
using UnityEngine;

namespace Scripts.UI.Displays
{
    public class ActionContentFiller : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;
        [SerializeField] private RectTransform _parent;
        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private List<ProjectContent> _standardProjects = new List<ProjectContent>();
        [SerializeField] private PatentContent _patentBasePrefab;
        [SerializeField] private ButtonSet _buttonSet;
        [SerializeField] private bool _debug;

        private List<GameObject> _activeContent = new List<GameObject>();
        private ActionCategory? _activeCategory;

        private bool _canInteract;

        private void Start()
        {
            PlayerTurnState.PlayerCanAct += UpdatePlayer;
            _gameData.Player.OnPatentsChanged += UpdateActions;
            _gameData.Player.OnTurnStart += UpdateActions;
            _gameData.Opponent.OnPatentsChanged += UpdateActions;
            _gameData.Opponent.OnTurnStart += UpdateActions;
        }

        private void OnDestroy()
        {
            PlayerTurnState.PlayerCanAct -= UpdatePlayer;
            _gameData.Player.OnPatentsChanged -= UpdateActions;
            _gameData.Player.OnTurnStart -= UpdateActions;
            _gameData.Opponent.OnPatentsChanged -= UpdateActions;
            _gameData.Opponent.OnTurnStart -= UpdateActions;
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
                case 4:
                    Fill(ActionCategory.SellPatents);
                    break;
            }
        }

        public void Fill(ActionCategory category)
        {
            if (_debug) Debug.Log("Currently Selected: " + category, gameObject);
            _activeCategory = category;
            foreach (var content in _activeContent) {
                Destroy(content);
            }
            bool projectsActive = category == ActionCategory.StandardProject;
            foreach (var project in _standardProjects) {
                project.gameObject.SetActive(projectsActive);
            }
            switch (category) {
                case ActionCategory.StandardProject:
                    _buttonSet.ForceSelect(0);
                    _headerText.text = "Projects";
                    foreach (var project in _standardProjects) {
                        project.SetInteractable(_canInteract);
                    }
                    break;
                case ActionCategory.OwnedPatents:
                    _buttonSet.ForceSelect(1);
                    _headerText.text = "Patents";
                    foreach (var patent in _gameData.CurrentPlayer.OwnedPatents) {
                        var newContent = Instantiate(_patentBasePrefab, _parent);
                        newContent.Fill(patent, _gameData);
                        _activeContent.Add(newContent.gameObject);
                    }
                    break;
                case ActionCategory.ActivePatents:
                    _buttonSet.ForceSelect(2);
                    _headerText.text = "Active Patents";
                    foreach (var patent in _gameData.CurrentPlayer.ActivePatents) {
                        var newContent = Instantiate(_patentBasePrefab, _parent);
                        newContent.Fill(patent, _gameData);
                        _activeContent.Add(newContent.gameObject);
                    }
                    break;
                case ActionCategory.CompletedPatents:
                    _buttonSet.ForceSelect(3);
                    _headerText.text = "Completed Patents";
                    foreach (var patent in _gameData.CurrentPlayer.CompletedPatents) {
                        var newContent = Instantiate(_patentBasePrefab, _parent);
                        newContent.Fill(patent, _gameData);
                        _activeContent.Add(newContent.gameObject);
                    }
                    break;
                case ActionCategory.SellPatents:
                    _buttonSet.DeselectAll();
                    _headerText.text = "Sell Patents";
                    foreach (var patent in _gameData.CurrentPlayer.OwnedPatents) {
                        var newContent = Instantiate(_patentBasePrefab, _parent);
                        newContent.Fill(patent, _gameData, true);
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
            Fill(category.Value);
        }
    }
}