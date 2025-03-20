using UnityEngine;

public class MuerteState : StateBase
{
    public MuerteState(StateOfGame next, IGameLoop mediator) : base(next, mediator)
    {
    }

    public override async Awaitable Enter()
    {
        Debug.Log($"{GetType()}");
        await Awaitable.NextFrameAsync();
    }

    public override async Awaitable Doing()
    {
        await Awaitable.WaitForSecondsAsync(2);
    }

    public override async Awaitable Exit()
    {
        await Awaitable.NextFrameAsync();
    }
}