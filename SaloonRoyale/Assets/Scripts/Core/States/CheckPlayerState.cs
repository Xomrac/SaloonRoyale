namespace Core.States
{
    public class CheckPlayerState : State
    {
        private Character _playerCharacter;
        
        public override void OnEnter(StateMachine stateMachine)
        {
            var healthComponent = _playerCharacter.health;
            var currentHealth = healthComponent.GetCurrentLife();
            
            if (currentHealth <= 0)
            {
                stateMachine.ChangeState(stateMachine.endGameState);
            }
        }

        public override void OnUpdate(StateMachine stateMachine)
        {
            // Not implemented
        }

        public override void OnExit(StateMachine stateMachine)
        {
            // Not implemented
        }
    }
}