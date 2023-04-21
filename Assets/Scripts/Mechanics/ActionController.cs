using System;
using Scripts.Data;
using Scripts.Enums;
using Scripts.UI.Displays;
using UnityEngine;

namespace Scripts.Mechanics
{
    public class ActionController : MonoBehaviour
    {
        [SerializeField] private GameData _gameData = null;
        [SerializeField] private ActionContentFiller _projectDisplay = null;
        [SerializeField] private ActionDetailsDisplay _actionDetails = null;
        [SerializeField] private PatentDetailsDisplay _patentDetails = null;
        [SerializeField] private GameObject _aiActions = null;

        private ActionCategory _activeSection = ActionCategory.Projects;

        #region Unity Functions

        private void OnEnable()
        {
            if (_gameData.Player.UserControlled) {
                _gameData.Player.OnTurnStart += ShowProjects;
                _gameData.Player.OnPatentsChanged += UpdateContext;
            } else {
                _gameData.Player.OnTurnStart += ShowAiDetails;
            }
            if (_gameData.Opponent.UserControlled) {
                _gameData.Opponent.OnTurnStart += ShowProjects;
                _gameData.Opponent.OnPatentsChanged += UpdateContext;
            } else {
                _gameData.Opponent.OnTurnStart += ShowAiDetails;
            }
        }

        private void OnDisable()
        {
            if (_gameData.Player.UserControlled) {
                _gameData.Player.OnTurnStart -= ShowProjects;
                _gameData.Player.OnPatentsChanged -= UpdateContext;
            } else {
                _gameData.Player.OnTurnStart -= ShowAiDetails;
            }
            if (_gameData.Opponent.UserControlled) {
                _gameData.Opponent.OnTurnStart -= ShowProjects;
                _gameData.Opponent.OnPatentsChanged -= UpdateContext;
            } else {
                _gameData.Opponent.OnTurnStart -= ShowAiDetails;
            }
        }

        #endregion

        public void ShowProjects()
        {
            _activeSection = ActionCategory.Projects;
            _projectDisplay.gameObject.SetActive(true);
            _actionDetails.gameObject.SetActive(false);
            _patentDetails.gameObject.SetActive(false);
            _aiActions.gameObject.SetActive(false);

            _projectDisplay.ShowProjects();
        }

        public void ShowPatents()
        {
            _activeSection = ActionCategory.Patents;
            _projectDisplay.gameObject.SetActive(true);
            _actionDetails.gameObject.SetActive(false);
            _patentDetails.gameObject.SetActive(false);
            _aiActions.gameObject.SetActive(false);

            _projectDisplay.ShowPatents();
        }

        public void ShowActionStandardProject(StandardProjectType type)
        {
            _activeSection = ActionCategory.ActionDetails;
            _projectDisplay.gameObject.SetActive(false);
            _actionDetails.gameObject.SetActive(true);
            _patentDetails.gameObject.SetActive(false);
            _aiActions.gameObject.SetActive(false);

            _actionDetails.ShowStandardProject(type);
        }

        public void ShowActionPlacingTile(TileType tile, int cost, bool patent)
        {
            _activeSection = ActionCategory.ActionDetails;
            _projectDisplay.gameObject.SetActive(false);
            _actionDetails.gameObject.SetActive(true);
            _patentDetails.gameObject.SetActive(false);
            _aiActions.gameObject.SetActive(false);

            _actionDetails.ShowPlacingTile(tile, cost, patent);
        }

        public void ShowPatentDetails(PatentData patent)
        {
            _activeSection = ActionCategory.PatentDetails;
            _projectDisplay.gameObject.SetActive(false);
            _actionDetails.gameObject.SetActive(false);
            _patentDetails.gameObject.SetActive(true);
            _aiActions.gameObject.SetActive(false);

            _patentDetails.Display(patent);
        }

        public void ShowAiDetails()
        {
            _activeSection = ActionCategory.Ai;
            _projectDisplay.gameObject.SetActive(false);
            _actionDetails.gameObject.SetActive(false);
            _patentDetails.gameObject.SetActive(false);
            _aiActions.gameObject.SetActive(true);
        }

        public void UpdateContext()
        {
            switch (_activeSection) {
                case ActionCategory.Projects:
                    _projectDisplay.UpdateContent();
                    break;
                case ActionCategory.Patents:
                    _projectDisplay.UpdateContent();
                    break;
                case ActionCategory.ActionDetails:
                    break;
                case ActionCategory.PatentDetails:
                    break;
                case ActionCategory.Ai:
                    break;
            }
        }
    }
}