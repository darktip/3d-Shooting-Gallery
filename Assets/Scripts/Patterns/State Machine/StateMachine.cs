using UnityEngine;

namespace Patterns.State_Machine
{
    public class StateMachine<T>
    {
        public State<T> CurrentState;
        public State<T> PreviousState;

        private T owner;
    
        public StateMachine(T owner)
        {
            this.owner = owner;
        }

        public void SetState(State<T> newState)
        {
            if (CurrentState != null)
                CurrentState.ExitState(owner);

            PreviousState = CurrentState;
            CurrentState = newState;

            Debug.Log(typeof(T).Name + " state changed: " + CurrentState.GetType().Name);
        
            CurrentState.EnterState(owner);
        }

        public void Update()
        {
            if (CurrentState != null)
                CurrentState.UpdateState(owner);
        }
    }
}
