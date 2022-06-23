using Sequencing;
using Sequencing.Points;
using UnityEngine;

namespace Core.States
{
    public class CheckSequenceState : State
    {
        [SerializeField] 
        private SequenceHandler sequenceHandler;
        
        private StateMachine _stateMachine;
        
        public override void OnEnter(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            
            void Continue(Point nextPoint)
            {
                sequenceHandler.OnArrivedPoint -= Continue;
                
                switch (nextPoint)
                {
                    case EmptyPoint:
                        _stateMachine.ChangeState(_stateMachine.checkSequenceState);
                        break;
                    case EnemyPoint:
                        _stateMachine.ChangeState(_stateMachine.playerState);
                        break;
                    case EndPoint:
                        _stateMachine.endGameState.SetCustomMessage("Congratulazioni! Hai liberato la cittadina!");
                        _stateMachine.ChangeState(_stateMachine.endGameState);
                        break;
                }
            }

            var currentPoint = sequenceHandler.GetCurrentPoint();
            if (currentPoint is EnemyPoint)
            {
                var enemyPoint = (EnemyPoint)currentPoint;
                var enemy = enemyPoint.GetEnemy();

                if (enemy.health.GetCurrentLife() <= 0)
                {
                    sequenceHandler.OnArrivedPoint += Continue;
                    sequenceHandler.GoToNextPointSequence();
                }
                else
                {
                    _stateMachine.ChangeState(_stateMachine.playerState);
                }
            }
            else
            {
                sequenceHandler.OnArrivedPoint += Continue;
                sequenceHandler.GoToNextPointSequence();
            }
        }

        public override void OnUpdate(StateMachine stateMachine){}

        public override void OnExit(StateMachine stateMachine) { }
    }
}