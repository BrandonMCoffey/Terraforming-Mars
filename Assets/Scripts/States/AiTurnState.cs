using Scripts.Data;
using Scripts.Mechanics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.States
{
    public class AiTurnState : State
    {
        private AiBrain _brain;

        private float _waitEndTime;
        private float _endTurnTime;

        public override void Enter()
        {
            _waitEndTime = Time.time + Random.Range(3f, 4.5f);
            _endTurnTime = Time.time + 5f;
            _brain.PlayerCanAct(true);
        }

        public override void Tick()
        {
            if (Time.time > _waitEndTime) {
                if (_brain.Think()) {
                    float additionalWait = Random.Range(2.5f, 3f);
                    _waitEndTime += additionalWait;
                    _endTurnTime += additionalWait + 0.1f;
                }
            }
            if (Time.time > _endTurnTime) {
                OnEndTurn();
            }
        }

        public override void Exit()
        {
            _brain.PlayerCanAct(false);
        }

        private void OnEndTurn()
        {
            StateMachine.NextTurn();
        }

        public void Setup(PlayerData playerData)
        {
            _brain = new AiBrain(StateMachine.GameData, playerData);
        }
    }
}