using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Enums;
using Scripts.Grid;
using Scripts.Structs;
using UnityEngine;
using Utility.Buttons;
using Utility.Inspector;

namespace Scripts.Data
{
    [CreateAssetMenu(menuName = "TM/Player Data")]
    public class PlayerData : ScriptableObject
    {
        [Header("Player Control")]
        [SerializeField] private bool _playerControlled = true;
        [SerializeField] private AiDifficultyLevels _aiLevel = AiDifficultyLevels.None;
        [Header("Basic Player Info")]
        [SerializeField] private string _defaultPlayerName = "Player";
        [SerializeField] private Color _defaultPlayerColor = Color.cyan;
        [SerializeField] [ReadOnly] private string _playerName = "Player";
        [SerializeField] [ReadOnly] private Color _playerColor = Color.cyan;

        [Header("Important Player Info")]
        [SerializeField] private SoundData _sounds;
        [SerializeField] private CorporationData _corporation;
        [SerializeField] [ReadOnly] private int _honor;

        [Header("Resources")]
        [SerializeField] private Resource _credits = new Resource(20);
        [SerializeField] private Resource _iron = new Resource(5);
        [SerializeField] private Resource _titanium = new Resource(0);
        [SerializeField] private Resource _plants = new Resource(10);
        [SerializeField] private Resource _energy = new Resource(5);
        [SerializeField] private Resource _heat = new Resource(0);

        [Header("Patents")]
        [SerializeField] private List<PatentData> _ownedPatents = new List<PatentData>();
        [SerializeField] [ReadOnly] private List<PatentData> _activePatents = new List<PatentData>();
        [SerializeField] [ReadOnly] private List<PatentData> _completedPatents = new List<PatentData>();

        public List<MilestoneType> Milestones { get; private set; }

        public SoundData SoundData => _sounds;
        public bool CurrentTurn { get; private set; }
        public int ActionsPerTurn => _corporation.ActionsPerTurn;
        public AiDifficultyLevels AiLevel => _aiLevel;
        public bool UserControlled => _playerControlled;
        public string DefaultName => _defaultPlayerName;

        private List<HexTile> _ownedTiles;

        public bool CanAct { get; set; }

        public List<HexTile> OwnedCities => _ownedTiles.Where(tile => tile.IsCity).ToList();
        public int OwnedForests => _ownedTiles.Count(tile => tile.IsForest);
        public int OwnedTiles => _ownedTiles.Count(tile => !tile.WaterTile);
        public int PlacedTiles => _ownedTiles.Count;

        public string PlayerName
        {
            get => _playerName;
            set => _playerName = value;
        }

        public Color DefaultColor => _defaultPlayerColor;

        public Color PlayerColor
        {
            get => _playerColor;
            set => _playerColor = value;
        }


        public List<PatentData> OwnedPatents => _ownedPatents;
        public List<PatentData> ActivePatents => _activePatents;
        public List<PatentData> CompletedPatents => _completedPatents;

        public event Action OnTurnStart;
        public event Action OnTurnEnd;
        public event Action OnHonorChanged;
        public event Action OnResourcesChanged;
        public event Action OnPatentsChanged;

#if UNITY_EDITOR
        private void OnValidate()
        {
            VerifyPatents();
        }
#endif

        #region Setup

        public void SetupPlayer(PatentCollection patents)
        {
            SetHonor(_corporation.StartHonor);
            // Resources
            SetResource(ResourceType.Credits, _corporation.StartCredits, true);
            SetResource(ResourceType.Iron, _corporation.StartIron, true);
            SetResource(ResourceType.Titanium, _corporation.StartTitanium, true);
            SetResource(ResourceType.Plant, _corporation.StartPlants, true);
            SetResource(ResourceType.Energy, _corporation.StartEnergy, true);
            SetResource(ResourceType.Heat, _corporation.StartHeat, true);
            // Resource Level
            SetResource(ResourceType.Credits, _corporation.StartCreditsLevel, true, true);
            SetResource(ResourceType.Iron, _corporation.StartIronLevel, true, true);
            SetResource(ResourceType.Titanium, _corporation.StartTitaniumLevel, true, true);
            SetResource(ResourceType.Plant, _corporation.StartPlantsLevel, true, true);
            SetResource(ResourceType.Energy, _corporation.StartEnergyLevel, true, true);
            SetResource(ResourceType.Heat, _corporation.StartHeatLevel, true, true);
            // Patents
            ClearAllPatents();
            AddPatents(patents.GetRandom(_corporation.StartPatents));
            _ownedTiles = new List<HexTile>();
            Milestones = new List<MilestoneType>();
        }

        public void ClaimMilestone(MilestoneType type)
        {
            Milestones.Add(type);
        }

        public void AddOwnedTile(HexTile tile)
        {
            _ownedTiles.Add(tile);
        }

        public void StartTurn()
        {
            CurrentTurn = true;
            OnTurnStart?.Invoke();
        }

        public void EndTurn()
        {
            CurrentTurn = false;
            OnTurnEnd?.Invoke();
        }

        #endregion

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

        public void ProductionPhase()
        {
            // Convert Energy to Heat
            AddResource(ResourceType.Heat, _energy.Amount);
            SetResource(ResourceType.Energy, 0);
            // Production
            AddResource(ResourceType.Credits, _honor + _credits.Level);
            AddResource(ResourceType.Iron, _iron.Level);
            AddResource(ResourceType.Titanium, _titanium.Level);
            AddResource(ResourceType.Plant, _plants.Level);
            AddResource(ResourceType.Energy, _energy.Level);
            AddResource(ResourceType.Heat, _heat.Level);
        }

        public int GetResource(ResourceType type, bool level = false)
        {
            return type switch
            {
                ResourceType.Credits  => level ? _credits.Level : _credits.Amount,
                ResourceType.Iron     => level ? _iron.Level : _iron.Amount,
                ResourceType.Titanium => level ? _titanium.Level : _titanium.Amount,
                ResourceType.Plant    => level ? _plants.Level : _plants.Amount,
                ResourceType.Energy   => level ? _energy.Level : _energy.Amount,
                ResourceType.Heat     => level ? _heat.Level : _heat.Amount,
                _                     => 0
            };
        }

        public bool HasResource(ResourceType type, int amount) => GetResource(type) >= amount;

        public void SetResource(ResourceType type, int amount, bool force = false, bool level = false)
        {
            if (!force && (amount < 0 || GetResource(type) == amount)) return;
            switch (type) {
                case ResourceType.Credits:
                    if (level) {
                        _credits.Level = amount;
                    } else {
                        _credits.Amount = amount;
                    }
                    break;
                case ResourceType.Iron:
                    if (level) {
                        _iron.Level = amount;
                    } else {
                        _iron.Amount = amount;
                    }
                    break;
                case ResourceType.Titanium:
                    if (level) {
                        _titanium.Level = amount;
                    } else {
                        _titanium.Amount = amount;
                    }
                    break;
                case ResourceType.Plant:
                    if (level) {
                        _plants.Level = amount;
                    } else {
                        _plants.Amount = amount;
                    }
                    break;
                case ResourceType.Energy:
                    if (level) {
                        _energy.Level = amount;
                    } else {
                        _energy.Amount = amount;
                    }
                    break;
                case ResourceType.Heat:
                    if (level) {
                        _heat.Level = amount;
                    } else {
                        _heat.Amount = amount;
                    }
                    break;
                default:
                    return;
            }
            OnResourcesChanged?.Invoke();
        }

        [Button(Spacing = 50)]
        public void AddResource(ResourceType type, int amount, bool level = false)
        {
            if (amount <= 0) return;
            SetResource(type, GetResource(type, level) + amount, true, level);
        }

        public bool RemoveResource(ResourceType type, int amount, bool level = false)
        {
            if (!level) {
                int check = GetResource(type);
                if (check < amount) return false;
            }
            SetResource(type, GetResource(type, level) - amount, true, level);
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
            foreach (var patent in patents.Where(patent => patent != null)) {
                _ownedPatents.Add(patent);
            }
            OnPatentsChanged?.Invoke();
        }

        public void SellPatent(PatentData patent)
        {
            if (!_ownedPatents.Contains(patent)) {
                Debug.Log("Attempting to sell patent that " + PlayerName + " does not own: " + (patent != null ? patent.Name : "null"));
                return;
            }
            _ownedPatents.Remove(patent);
            AddResource(ResourceType.Credits, 1);
        }

        public void CompletePatent(PatentData patent)
        {
            if (!_ownedPatents.Contains(patent)) return;
            _ownedPatents.Remove(patent);
            _completedPatents.Add(patent);
        }

        public bool ActivateFirstPatent(GameData gameData)
        {
            if (_ownedPatents.Count == 0) return false;
            var patent = _ownedPatents[0];
            if (!patent.AnyActivate(gameData)) {
                _ownedPatents.Remove(patent);
                _ownedPatents.Add(patent);
                return false;
            }
            CompletePatent(patent);
            return true;
        }

        #endregion
    }
}