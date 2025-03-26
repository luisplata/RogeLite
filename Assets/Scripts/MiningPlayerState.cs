using UnityEngine;

public class MiningPlayerState : StateBasePlayer
{
    public MiningPlayerState(StateOfGame next, IPlayerMediator mediator) : base(next, mediator)
    {
    }

    public override async Awaitable Enter()
    {
        Debug.Log($"Enter to {GetType()}");
        whileTrue = true;
        await Awaitable.NextFrameAsync();
    }

    public override async Awaitable Doing()
    {
        while (whileTrue)
        {
            await Awaitable.WaitForSecondsAsync(0.2f);
            //Try to get minerals
            if (!_mediator.ICantGetMinerals)
            {
                whileTrue = false;
                nextState = StateOfGame.PLAYER_WALK;
            }
            else
            {
                await Awaitable.WaitForSecondsAsync(_mediator.GetTimeToMining());
                if (!_mediator.ICantGetMinerals)
                {
                    whileTrue = false;
                    nextState = StateOfGame.PLAYER_WALK;
                    break;
                }                                            

                _mediator.GetMinerals();
            }
        }
    }

    public override async Awaitable Exit()
    {
        await Awaitable.NextFrameAsync();
    }
}