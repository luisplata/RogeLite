using UnityEngine;

public class WalkPlayerState : StateBasePlayer
{
    public WalkPlayerState(StateOfGame next, IPlayerMediator mediator) : base(next, mediator)
    {
    }

    public override async Awaitable Enter()
    {
        Debug.Log($"Enter to {GetType()}");
        whileTrue = true;
        await Awaitable.NextFrameAsync();
        _mediator.CanMove(true);
    }


    public override async Awaitable Doing()
    {
        while (whileTrue)
        {
            await Awaitable.WaitForSecondsAsync(0.2f);
            if (_mediator.ICantGetMinerals)
            {
                whileTrue = false;
                nextState = StateOfGame.PLAYER_MINING;
            }
        }
    }

    public override async Awaitable Exit()
    {
        await Awaitable.NextFrameAsync();
    }
}