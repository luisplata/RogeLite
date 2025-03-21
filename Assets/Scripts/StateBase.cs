using UnityEngine;

public abstract class StateBase : IGameState
{
    protected StateOfGame nextState;

    public StateBase(StateOfGame next)
    {
        nextState = next;
    }

    public abstract Awaitable Enter();
    public abstract Awaitable Doing();
    public abstract Awaitable Exit();

    public StateOfGame NextState()
    {
        return nextState;
    }
}