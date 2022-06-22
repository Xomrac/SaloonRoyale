using Core.States;
using UnityEngine;

namespace Core
{
    public class StateMachine : MonoBehaviour
    {
        public StartGameState startGameState;
        public CheckSequenceState checkSequenceState;
        public PlayerState playerState;
        public EnemyState enemyState;
        public ProcessCardState processCardState;
        public CheckPlayerState checkPlayerState;
        public CheckEnemyState checkEnemyState;
        public EndGameState endGameState;
        
        /// <summary>
        /// The current game state.
        /// </summary>
        private State _currentState;

        /// <summary>
        /// Set the first game state.
        /// </summary>
        private void Start()
        {
            ChangeState(startGameState);
        }

        /// <summary>
        /// Which state update is executed
        /// </summary>
        public void Update()
        {
            if (_currentState)
            {
                _currentState.OnUpdate(this);
            }
        }

        /// <summary>
        /// Change the current executed state
        /// </summary>
        /// <param name="state"></param>
        public void ChangeState(State state)
        {
            if (_currentState != null)
            {
                _currentState.OnExit(this);
            }
            
            _currentState = state;

            if (_currentState != null)
            {
                _currentState.OnEnter(this);
            }
        }
    }
}