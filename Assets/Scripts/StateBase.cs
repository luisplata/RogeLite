using UnityEngine;

public abstract class StateBase : IGameState
{
    protected StateOfGame nextState;
    protected IGameLoop gameLoop;

    public StateBase(StateOfGame next, IGameLoop mediator)
    {
        nextState = next;
        gameLoop = mediator;
    }

    public abstract Awaitable Enter();
    public abstract Awaitable Doing();
    public abstract Awaitable Exit();

    public StateOfGame NextState()
    {
        return nextState;
    }
}