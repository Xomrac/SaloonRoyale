using System.Collections;
using Sequencing;
using Sequencing.Points;
using UnityEngine;

namespace Core.States
{
    public class CheckSequenceState : State
    {
        [SerializeField] private SequenceHandler sequenceHandler;

        private StateMachine _stateMachine;
        private Coroutine _goUntilNextValidEnemy;
        
        public override void OnEnter(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _goUntilNextValidEnemy = StartCoroutine(GoUntilNextValidEnemyState());
        }

        public override void OnUpdate(StateMachine stateMachine){}

        public override void OnExit(StateMachine stateMachine)
        {
            StopCoroutine(_goUntilNextValidEnemy);
        }

        private IEnumerator GoUntilNextValidEnemyState()
        {
            var wait = false;

            void Continue(Point nextPoint)
            {
                wait = false;
            }
            
            while (true)
            {
                yield return new WaitUntil(() => !wait);
                
                var currentPoint = sequenceHandler.GetCurrentPoint();
                switch (currentPoint)
                {
                    case StartPoint:
                    case EmptyPoint:
                        wait = true;
                        sequenceHandler.OnArrivedPoint += Continue;
                        sequenceHandler.GoToNextPointSequence();
                        continue;
                    case EnemyPoint enemyPoint:
                        var enemy = enemyPoint.GetEnemy();
                        if (enemy.health.GetCurrentLife() <= 0)
                        {
                            wait = true;
                            sequenceHandler.OnArrivedPoint += Continue;
                            sequenceHandler.GoToNextPointSequence();
                        }
                        else
                        {
                            _stateMachine.ChangeState(_stateMachine.playerState);
                            yield break;
                        }
                        break;
                    case EndPoint:
                        _stateMachine.ChangeState(_stateMachine.endGameState);
                        yield break;
                }
            }
        }
    }
}