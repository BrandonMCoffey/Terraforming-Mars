using System;
using System.Collections;
using Scripts.Grid;
using Scripts.StateMachine;
using UnityEngine;

namespace Scripts.States
{
    public class EnemyTurnTbState : TbState
    {
        [SerializeField] private EnemyToGrid _enemyToGrid = null;
        [SerializeField] private float _thinkTime = 3f;
        [SerializeField] private float _continuePause = 1f;

        public static event Action TurnBegin;
        public static event Action TurnEnd;

        public override void Enter()
        {
            TurnBegin?.Invoke();

            StartCoroutine(EnemyThinkRoutine());
        }

        private IEnumerator EnemyThinkRoutine()
        {
            yield return new WaitForSecondsRealtime(_thinkTime);

            _enemyToGrid.Act();

            yield return new WaitForSecondsRealtime(_continuePause);

            TurnEnd?.Invoke();
            StateMachine.ChangeState<PlayerTurnTbState>();
        }
    }
}