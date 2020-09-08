namespace Patterns.State_Machine
{
    // base class for states
    // callbacks on enter, exit and update
    // virtual for no need to override
    public abstract class State<T>
    {
        protected StateMachine<T> stateMachine;

        public State(StateMachine<T> stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public virtual void EnterState(T owner)
        {
        }

        public virtual void ExitState(T owner)
        {
        }

        public virtual void UpdateState(T owner)
        {
        }
    }
}
