namespace Core.States
{
    public class CheckPlayerState : State
    {
        private Health _playerHealth;
        public override void OnEnter(StateMachine stateMachine)
        {
            //
        }

        public override void OnUpdate(StateMachine stateMachine)
        {
            var currentHealth = _playerHealth.GetCurrentLife();

            if (currentHealth <= 0)
            {
                stateMachine.ChangeState(new EndGameState());
            }
        }

        public override void OnExit(StateMachine stateMachine)
        {
            //
        }
    }
}