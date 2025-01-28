using Player.Movement.StateMachine;
using UnityEngine;

namespace Common.StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        protected State CurrentState { get; private set; }

        public void SwitchState(State state)
        {
            CurrentState?.Exit();
            CurrentState = state;
            CurrentState.Enter();
        }

        private void Update()
        {
            CurrentState?.Tick();
        }
        
    }
}