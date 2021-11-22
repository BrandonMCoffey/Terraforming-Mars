using System;
using System.Collections.Generic;
using System.Linq;
using GridTool.DataScripts;
using UnityEngine;
using Utility.Buttons;

namespace Scripts.Data
{
    [CreateAssetMenu(menuName = "TM/Player Data")]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] [ReadOnly] private int _credits = 20;

        [SerializeField] [ReadOnly] private List<PatentData> _ownedPatents = new List<PatentData>();
        [SerializeField] [ReadOnly] private List<PatentData> _activePatents = new List<PatentData>();
        [SerializeField] [ReadOnly] private List<PatentData> _completedPatents = new List<PatentData>();

        [Header("Debug Menu")]
        [SerializeField] private bool _debug;

        public List<PatentData> OwnedPatents => _ownedPatents;
        public List<PatentData> ActivePatents => _activePatents;
        public List<PatentData> CompletedPatents => _completedPatents;

        public event Action<int> OnCreditsChanged;
        public event Action OnPatentsChanged;
        public event Action OnAnythingChanged;

        private void OnValidate()
        {
            VerifyPatents();
        }

        #region Credits

        public bool HasCredits(int amount) => _credits >= amount;

        public void SetCredits(int amount)
        {
            if (amount < 0) return;
            ModifyCredits(amount);
        }

        public void AddCredits(int amount)
        {
            if (amount <= 0) return;
            ModifyCredits(_credits + amount);
        }

        public bool RemoveCredits(int amount)
        {
            if (_credits < amount) return false;
            ModifyCredits(_credits - amount);
            return true;
        }

        private void ModifyCredits(int creditAmount)
        {
            if (creditAmount == _credits) return;
            _credits = creditAmount;
            OnCreditsChanged?.Invoke(_credits);
            OnAnythingChanged?.Invoke();
        }

        #endregion

        #region Patents

        public bool HasAvailablePatents() => OwnedPatents.Count > 0;

        private void VerifyPatents()
        {
            _ownedPatents = _ownedPatents.Where(patent => patent != null).ToList();
            _activePatents = _activePatents.Where(patent => patent != null).ToList();
            _completedPatents = _completedPatents.Where(patent => patent != null).ToList();
        }

        [Button]
        public void AddPatent(PatentData patent)
        {
            if (patent == null) return;
            _ownedPatents.Add(patent);
            OnPatentsChanged?.Invoke();
            OnAnythingChanged?.Invoke();
        }

        [Button]
        public PatentData RemoveFirstPatent()
        {
            if (_ownedPatents.Count == 0) return null;
            var patent = _ownedPatents[0];
            _ownedPatents.Remove(patent);
            OnPatentsChanged?.Invoke();
            OnAnythingChanged?.Invoke();
            return patent;
        }

        [Button]
        public void ActivateFirstPatent()
        {
            if (_ownedPatents.Count == 0) return;
            var patent = _ownedPatents[0];
            _ownedPatents.Remove(patent);
            _activePatents.Add(patent);
            OnPatentsChanged?.Invoke();
            OnAnythingChanged?.Invoke();
        }

        #endregion
    }
}