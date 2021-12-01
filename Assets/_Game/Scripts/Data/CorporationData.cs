using UnityEngine;

namespace Scripts.Data
{
    [CreateAssetMenu(menuName = "TM/Corporation Data")]
    public class CorporationData : ScriptableObject
    {
        [SerializeField] [Range(0, 10)] private int _actionsPerTurn = 2;
        [SerializeField] [Range(0, 100)] private int _startHonor;
        [SerializeField] [Range(0, 20)] private int _startPatents = 10;
        [SerializeField] [Range(0, 100)] private int _startCredits = 20;
        [SerializeField] [Range(0, 100)] private int _startIron = 5;
        [SerializeField] [Range(0, 100)] private int _startTitanium;
        [SerializeField] [Range(0, 100)] private int _startPlants = 10;
        [SerializeField] [Range(0, 100)] private int _startEnergy = 5;
        [SerializeField] [Range(0, 100)] private int _startHeat;

        public int ActionsPerTurn => _actionsPerTurn;
        public int StartHonor => _startHonor;
        public int StartPatents => _startPatents;
        public int StartCredits => _startCredits;
        public int StartIron => _startIron;
        public int StartTitanium => _startTitanium;
        public int StartPlants => _startPlants;
        public int StartEnergy => _startEnergy;
        public int StartHeat => _startHeat;
    }
}