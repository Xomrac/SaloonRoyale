using UnityEngine;

namespace Core
{
    public class StateMachine : MonoBehaviour
    {
        private State _currentState;

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

        /// <summary>
        /// Which state update need to be executed
        /// </summary>
        public void Update()
        {
            if (_currentState)
            {
                _currentState.OnUpdate(this);
            }
        }
    }
}