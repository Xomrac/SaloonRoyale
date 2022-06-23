using UnityEngine;

namespace Core.States
{
    public class CheckPlayerState : State
    {
        [SerializeField] Character _playerCharacter;
        
        public override void OnEnter(StateMachine stateMachine)
        {
            var healthComponent = _playerCharacter.health;
            var currentHealth = healthComponent.GetCurrentLife();
            
            if (currentHealth <= 0)
            {
                stateMachine.endGameState.SetCustomMessage("Hai perso miseramente!");
                stateMachine.ChangeState(stateMachine.endGameState);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.checkEnemyState);
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