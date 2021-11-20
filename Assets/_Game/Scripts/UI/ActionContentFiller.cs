using System;
using System.Collections.Generic;
using Scripts.Data;
using UnityEngine;

namespace Scripts.UI
{
    public class ActionContentFiller : MonoBehaviour
    {
        [SerializeField] private PlayerPatentData _playerData = null;
        [SerializeField] private RectTransform _parent = null;
        [SerializeField] private ProjectContent _projectBasePrefab = null;
        [SerializeField] private PatentContent _patentBasePrefab = null;

        private List<GameObject> _activeContent = new List<GameObject>();
        private ActionCategory? _activeCategory;

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
            switch (category) {
                case ActionCategory.StandardProject:
                    break;
                case ActionCategory.OwnedPatents:
                    foreach (var patent in _playerData.ActivePatents) {
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
                    foreach (var patent in _playerData.ActivePatents) {
                        var newContent = Instantiate(_patentBasePrefab, _parent);
                        newContent.Fill(patent);
                        _activeContent.Add(newContent.gameObject);
                    }
                    break;
            }
        }
    }
}