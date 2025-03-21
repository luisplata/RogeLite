using UnityEngine;

public class InitPlayerState : StateBasePlayer
{
    public InitPlayerState(StateOfGame next, IPlayerMediator mediator) : base(next, mediator)
    {
    }

    public override async Awaitable Enter()
    {
        Debug.Log($"Enter to {GetType()}");
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