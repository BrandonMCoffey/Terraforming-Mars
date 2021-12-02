using UnityEngine;

namespace Scripts.Data
{
    [CreateAssetMenu(menuName = "TM/Corporation Data")]
    public class CorporationData : ScriptableObject
    {
        [SerializeField] [Range(0, 10)] private int _actionsPerTurn = 2;
        [SerializeField] [Range(0, 100)] private int _startHonor;
        [SerializeField] [Range(0, 20)] private int _startPatents = 10;
        [Header("Starting Resource Amount")]
        [SerializeField] [Range(0, 100)] private int _startCredits = 20;
        [SerializeField] [Range(0, 100)] private int _startIron = 5;
        [SerializeField] [Range(0, 100)] private int _startTitanium = 0;
        [SerializeField] [Range(0, 100)] private int _startPlants = 10;
        [SerializeField] [Range(0, 100)] private int _startEnergy = 5;
        [SerializeField] [Range(0, 100)] private int _startHeat = 0;
        [Header("Starting Resource Level")]
        [SerializeField] [Range(0, 100)] private int _startCreditsLevel = 1;
        [SerializeField] [Range(0, 100)] private int _startIronLevel = 1;
        [SerializeField] [Range(0, 100)] private int _startTitaniumLevel = 1;
        [SerializeField] [Range(0, 100)] private int _startPlantsLevel = 1;
        [SerializeField] [Range(0, 100)] private int _startEnergyLevel = 1;
        [SerializeField] [Range(0, 100)] private int _startHeatLevel = 1;

        public int ActionsPerTurn => _actionsPerTurn;
        public int StartHonor => _startHonor;
        public int StartPatents => _startPatents;
        public int StartCredits => _startCredits;
        public int StartIron => _startIron;
        public int StartTitanium => _startTitanium;
        public int StartPlants => _startPlants;
        public int StartEnergy => _startEnergy;
        public int StartHeat => _startHeat;
        public int StartCreditsLevel => _startCreditsLevel;
        public int StartIronLevel => _startIronLevel;
        public int StartTitaniumLevel => _startTitaniumLevel;
        public int StartPlantsLevel => _startPlantsLevel;
        public int StartEnergyLevel => _startEnergyLevel;
        public int StartHeatLevel => _startHeatLevel;
    }
}