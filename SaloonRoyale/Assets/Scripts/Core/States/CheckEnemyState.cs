using Sequencing;

namespace Core.States
{
    public class CheckEnemyState : State
    {
        private Character _enemyCharacter;
        public override void OnEnter(StateMachine stateMachine)
        {
            var healthComponent = _enemyCharacter.health;
            var currentHealth = healthComponent.GetCurrentLife();
            
            if (currentHealth <= 0)
            {
                Destroy(_enemyCharacter.gameObject);
            }
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