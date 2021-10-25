using System;
using System.Collections;
using Scripts.CardGame.StateMachine;
using UnityEngine;

namespace Scripts.CardGame.States
{
    public class EnemyTurnCardGameState : CardGameState
    {
        [SerializeField] private float _pauseDuration = 1.5f;

        public static event Action TurnBegin;
        public static event Action TurnEnd;

        public override void Enter()
        {
            TurnBegin?.Invoke();

            StartCoroutine(EnemyThinkRoutine(_pauseDuration));
        }

        private IEnumerator EnemyThinkRoutine(float thinkTime)
        {
            yield return new WaitForSecondsRealtime(thinkTime);

            TurnEnd?.Invoke();
            StateMachine.ChangeState<PlayerTurnCardGameState>();
        }
    }
}