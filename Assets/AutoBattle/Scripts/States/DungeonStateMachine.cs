public class DungeonStateMachine
{
    private IDungeonState currentState;

    public void SetState(IDungeonState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
}