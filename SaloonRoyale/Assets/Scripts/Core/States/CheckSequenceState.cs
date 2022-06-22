using Sequencing;
using Sequencing.Points;
using UnityEngine;

namespace Core.States
{
    public class CheckSequenceState : State
    {
        [SerializeField] private SequenceHandler sequenceHandler;

        private StateMachine _stateMachine;
        
        public override void OnEnter(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            
            void Continue(Point nextPoint)
            {
                switch (nextPoint)
                {
                    case StartPoint:
                    case EmptyPoint:
                        sequenceHandler.OnArrivedPoint -= Continue;
                        _stateMachine.ChangeState(_stateMachine.checkSequenceState);
                        break;
                    case EnemyPoint:
                        _stateMachine.ChangeState(_stateMachine.playerState);
                        break;
                }
            }
            
            sequenceHandler.OnArrivedPoint += Continue;
            sequenceHandler.GoToNextPointSequence();
        }

        public override void OnUpdate(StateMachine stateMachine){}

        public override void OnExit(StateMachine stateMachine) { }
    }
}