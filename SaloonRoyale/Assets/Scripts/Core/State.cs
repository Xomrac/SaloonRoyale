using UnityEngine;

namespace Core
{
    public abstract class State : MonoBehaviour
    {
        public abstract void OnEnter(StateMachine stateMachine);
        public abstract void OnUpdate(StateMachine stateMachine);
        public abstract void OnExit(StateMachine stateMachine);
    }
}