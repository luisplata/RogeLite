using UnityEngine;

public class InitState : StateBaseGameLoop
{
    public InitState(StateOfGame next, IGameLoop mediator) : base(next, mediator)
    {
    }

    public override async Awaitable Enter()
    {
        Debug.Log($"{GetType()}");
        await Awaitable.NextFrameAsync();
        gameLoop.HideGameOverUi();
    }

    public override async Awaitable Doing()
    {
        //Debug.Log("Doing");
        while (!gameLoop.StartGame)
        {
            await Awaitable.WaitForSecondsAsync(0.5f);
        }
    }

    public override async Awaitable Exit()
    {
        await Awaitable.NextFrameAsync();
    }
}