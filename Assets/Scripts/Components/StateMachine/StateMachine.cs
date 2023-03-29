public class StateMachine : IStateMachine
{
    public IState CurrentState { get; private set; }

    public void Initialize(IState startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void ChangeState(IState nextState)
    {
        CurrentState.Exit();

        CurrentState = nextState;
        nextState.Enter();
    }
}
