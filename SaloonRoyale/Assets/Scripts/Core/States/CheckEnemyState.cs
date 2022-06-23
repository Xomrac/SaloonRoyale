using Sequencing;
using Sequencing.Points;
using UnityEngine;

namespace Core.States
{
    public class CheckEnemyState : State
    {
        private Character _enemyCharacter;
        [SerializeField] private SequenceHandler sequencer;
        public override void OnEnter(StateMachine stateMachine)
        {
            _enemyCharacter=(sequencer.GetCurrentPoint() as EnemyPoint).GetEnemy();
            var healthComponent = _enemyCharacter.health;
            var currentHealth = healthComponent.GetCurrentLife();
            
            if (currentHealth <= 0)
            {
                Destroy(_enemyCharacter.gameObject);
            }
            stateMachine.ChangeState(stateMachine.checkSequenceState);
        }

        public override void OnUpdate(StateMachine stateMachine)
        {
            //Not Implemented
        }

        public override void OnExit(StateMachine stateMachine)
        {
            //Not Implemented
        }
    }
}