using UnityEngine;

public class SubidaDeNivelState : StateBaseGameLoop
{
    public SubidaDeNivelState(StateOfGame next, IGameLoop mediator) : base(next, mediator)
    {
    }

    public override async Awaitable Enter()
    {
        await Awaitable.NextFrameAsync();
    }

    public override async Awaitable Doing()
    {
        await Awaitable.NextFrameAsync();
    }

    public override async Awaitable Exit()
    {
        await Awaitable.NextFrameAsync();
    }
}