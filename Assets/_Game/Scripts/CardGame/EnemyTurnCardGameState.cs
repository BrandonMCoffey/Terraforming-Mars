using System;
using System.Collections;
using Scripts.StateMachine;
using UnityEngine;

namespace Scripts.CardGame
{
    public class EnemyTurnCardGameState : CardGameState
    {
        [SerializeField] private float _pauseDuration = 1.5f;

        public static event Action EnemyTurnBegin;
        public static event Action EnemyTurnEnd;

        public override void Enter()
        {
            EnemyTurnBegin?.Invoke();

            StartCoroutine(EnemyThinkRoutine(_pauseDuration));
        }

        private IEnumerator EnemyThinkRoutine(float thinkTime)
        {
            yield return new WaitForSecondsRealtime(thinkTime);

            EnemyTurnEnd?.Invoke();
            StateMachine.ChangeState<PlayerTurnCardGameState>();
        }
    }
}