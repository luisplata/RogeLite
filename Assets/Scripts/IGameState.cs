using UnityEngine;

public interface IGameState
{
    Awaitable Enter();
    Awaitable Doing();
    Awaitable Exit();

    StateOfGame NextState();
}