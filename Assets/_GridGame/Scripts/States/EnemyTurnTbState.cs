using System;
using System.Collections;
using Scripts.GridActions;
using Scripts.StateMachine;
using UnityEngine;

namespace Scripts.States
{
    public class EnemyTurnTbState : TbState
    {
        [SerializeField] private EnemyToGrid _enemyToGrid = null;
        [SerializeField] private float _thinkTime = 3f;
        [SerializeField] private float _continuePause = 1f;

        public static event Action<int> TurnBegin;
        public static event Action TurnEnd;

        private int _turnCount;
        private bool _lostGame;

        public override void Enter()
        {
            if (_enemyToGrid.CheckGameOver) {
                _lostGame = true;
                return;
            }

            TurnBegin?.Invoke(++_turnCount);

            StartCoroutine(EnemyThinkRoutine());
        }

        public override void Tick()
        {
            if (_lostGame) {
                _lostGame = false;
                StateMachine.ChangeState<WinTbState>();
            }
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