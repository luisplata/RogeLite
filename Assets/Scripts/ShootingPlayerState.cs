﻿using UnityEngine;

public class ShootingPlayerState : StateBasePlayer
{
    public ShootingPlayerState(StateOfGame next, IPlayerMediator mediator) : base(next, mediator)
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