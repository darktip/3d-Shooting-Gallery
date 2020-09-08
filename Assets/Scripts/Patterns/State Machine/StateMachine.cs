using UnityEngine;

namespace Patterns.State_Machine
{
    // State machine to control gameplay
    public class StateMachine<T>
    {
        public State<T> CurrentState;        // current 
        public State<T> PreviousState;       // and previous states

        private T owner;
    
        public StateMachine(T owner)        // passing owner through constructor
        {
            this.owner = owner;
        }

        public void SetState(State<T> newState)    // setting new state
        {
            if (CurrentState != null)
                CurrentState.ExitState(owner);        // exiting current

            PreviousState = CurrentState;             // setting previous
            CurrentState = newState;

            Debug.Log(typeof(T).Name + " state changed: " + CurrentState.GetType().Name);
        
            CurrentState.EnterState(owner);           // entering new state
        }

        public void Update()                           // update current state if not null
        {
            if (CurrentState != null)
                CurrentState.UpdateState(owner);
        }
    }
}
