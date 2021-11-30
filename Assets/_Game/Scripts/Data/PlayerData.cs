using System;
using System.Collections.Generic;
using System.Linq;
using GridTool.DataScripts;
using Scripts.Enums;
using UnityEngine;
using Utility.Buttons;

namespace Scripts.Data
{
    [CreateAssetMenu(menuName = "TM/Player Data")]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private CorporationData _corporation = null;
        [SerializeField] private Color _color = Color.cyan;
        [SerializeField] [ReadOnly] private int _honor;

        [Header("Resources")]
        [SerializeField] [ReadOnly] private int _credits;
        [SerializeField] [ReadOnly] private int _iron;
        [SerializeField] [ReadOnly] private int _titanium;
        [SerializeField] [ReadOnly] private int _plants;
        [SerializeField] [ReadOnly] private int _energy;
        [SerializeField] [ReadOnly] private int _heat;

        [Header("Resources")]
        [SerializeField] [ReadOnly] private List<PatentData> _ownedPatents = new List<PatentData>();
        [SerializeField] [ReadOnly] private List<PatentData> _activePatents = new List<PatentData>();
        [SerializeField] [ReadOnly] private List<PatentData> _completedPatents = new List<PatentData>();

        [Header("Debug Menu")]
        [SerializeField] private bool _debug;

        public int ActionsPerTurn => _corporation.ActionsPerTurn;

        public Color PlayerColor => _color;
        public List<PatentData> OwnedPatents => _ownedPatents;
        public List<PatentData> ActivePatents => _activePatents;
        public List<PatentData> CompletedPatents => _completedPatents;

        public event Action OnHonorChanged;
        public event Action<int> OnCreditsChanged;
        public event Action OnResourcesChanged;
        public event Action OnPatentsChanged;

#if UNITY_EDITOR
        private void OnValidate()
        {
            VerifyPatents();
        }
#endif

        public void SetupPlayer(PatentCollection patents)
        {
            SetHonor(_corporation.StartHonor);
            // Resources
            SetResource(ResourceType.Credits, _corporation.StartCredits);
            SetResource(ResourceType.Iron, _corporation.StartIron);
            SetResource(ResourceType.Titanium, _corporation.StartTitanium);
            SetResource(ResourceType.Plant, _corporation.StartPlants);
            SetResource(ResourceType.Energy, _corporation.StartEnergy);
            SetResource(ResourceType.Heat, _corporation.StartHeat);
            // Patents
            ClearAllPatents();
            AddPatents(patents.GetRandom(_corporation.StartPatents));
        }

        #region Honor

        public int Honor => _honor;

        public void SetHonor(int amount)
        {
            _honor = amount;
            OnHonorChanged?.Invoke();
        }

        public void AddHonor(int amount)
        {
            if (amount <= 0) return;
            _honor += amount;
            OnHonorChanged?.Invoke();
        }

        #endregion

        #region Resources

        public int GetResource(ResourceType type)
        {
            return type switch
            {
                ResourceType.Credits  => _credits,
                ResourceType.Iron     => _iron,
                ResourceType.Titanium => _titanium,
                ResourceType.Plant    => _plants,
                ResourceType.Energy   => _energy,
                ResourceType.Heat     => _heat,
                _                     => 0
            };
        }

        public bool HasResource(ResourceType type, int amount) => GetResource(type) >= amount;

        public void SetResource(ResourceType type, int amount, bool force = false)
        {
            if (!force && (amount < 0 || GetResource(type) == amount)) return;
            switch (type) {
                case ResourceType.Credits:
                    _credits = amount;
                    OnCreditsChanged?.Invoke(_credits);
                    break;
                case ResourceType.Iron:
                    _iron = amount;
                    break;
                case ResourceType.Titanium:
                    _titanium = amount;
                    break;
                case ResourceType.Plant:
                    _plants = amount;
                    break;
                case ResourceType.Energy:
                    _energy = amount;
                    break;
                case ResourceType.Heat:
                    _heat = amount;
                    break;
                default:
                    return;
            }
            OnResourcesChanged?.Invoke();
        }

        [Button]
        public void AddResource(ResourceType type, int amount)
        {
            if (amount <= 0) return;
            SetResource(type, GetResource(type) + amount, true);
        }

        public bool RemoveResource(ResourceType type, int amount)
        {
            int check = GetResource(type);
            if (check < amount) return false;
            SetResource(type, check - amount, true);
            return true;
        }

        #endregion

        #region Patents

        public void ClearAllPatents()
        {
            _ownedPatents = new List<PatentData>();
            _activePatents = new List<PatentData>();
            _completedPatents = new List<PatentData>();
        }

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
        }

        public void AddPatents(List<PatentData> patents)
        {
            foreach (var patent in patents) {
                _ownedPatents.Add(patent);
            }
            OnPatentsChanged?.Invoke();
        }

        [Button]
        public PatentData RemoveFirstPatent()
        {
            if (_ownedPatents.Count == 0) return null;
            var patent = _ownedPatents[0];
            _ownedPatents.Remove(patent);
            OnPatentsChanged?.Invoke();
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
        }

        #endregion
    }
}