using System.Collections.Generic;
using System.Linq;
using Scripts.Enums;
using Scripts.Structs;
using UnityEngine;

namespace Scripts.Mechanics
{
    [CreateAssetMenu(menuName = "TM/AI Logic")]
    public class AiLogic : ScriptableObject
    {
        [SerializeField] private int _actionsPerTurn = 2;
        [SerializeField] private List<WeightedAiAction> _actions = new List<WeightedAiAction>();

        public int ActionsPerTurn => _actionsPerTurn;
        public List<WeightedAiAction> WeightedActions => _actions;

        public AiActions GetWeightedRandom()
        {
            if (_actions.Count == 0) return AiActions.None;
            float sum = _actions.Sum(action => action.Weight);
            float rand = Random.Range(0f, sum);
            foreach (var action in _actions) {
                if (rand < action.Weight) {
                    return action.Action;
                }
                rand -= action.Weight;
            }
            return _actions[0].Action;
        }
    }
}