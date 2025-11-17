public class StateMachine
{
    public EntityState CurrentState { get; private set; }

    public void Initialize(EntityState startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }

    public void ChangeState(EntityState state)
    {
        CurrentState.Exit();
        CurrentState = state;
        CurrentState.Enter();
    }

    public void UpdateActiveState()
    {

        CurrentState.Update();
    }
}
