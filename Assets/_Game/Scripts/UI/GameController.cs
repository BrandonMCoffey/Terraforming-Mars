using System;
using Scripts.Data;
using Scripts.Enums;
using Scripts.Mechanics;
using Scripts.UI.Displays;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;

        [SerializeField] private GameData _gameData;
        [SerializeField] private PlanetData _planet;
        [SerializeField] private IconData _icons;
        [SerializeField] private ActionController _actionController;

        public static Action OnConfirmAction;
        public static Action OnCancelAction;

        private PatentData _currentPatent;

        private void Awake()
        {
            Instance = this;
        }

        public bool IncreasePlanetStatus(PlanetStatusType type)
        {
            return _planet.IncreaseStatus(type);
        }

        public void ShowPlacingTile(TileType tile, int cost, bool patent)
        {
            _currentPatent = null;
            _actionController.ShowActionPlacingTile(tile, cost, patent);
        }

        public void ShowStandardProject(StandardProjectType type)
        {
            _currentPatent = null;
            _actionController.ShowActionStandardProject(type);
        }

        public void ShowPatentDetails(PatentData patent)
        {
            _currentPatent = patent;
            _actionController.ShowPatentDetails(patent);
        }

        public void ShowActions(bool showProjects = true)
        {
            _currentPatent = null;
            if (showProjects) {
                _actionController.ShowProjects();
            } else {
                _actionController.ShowPatents();
            }
        }

        public void ConfirmAction()
        {
            OnConfirmAction?.Invoke();
        }

        public void CancelAction()
        {
            OnCancelAction?.Invoke();
            ShowActions();
        }

        public void CancelPatent()
        {
            OnCancelAction?.Invoke();
            ShowActions(false);
        }
    }
}