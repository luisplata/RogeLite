using UnityEngine;

public class EndGameState : StateBaseGameLoop
{
    public EndGameState(StateOfGame next, IGameLoop mediator) : base(next, mediator)
    {
    }

    public override async Awaitable Enter()
    {
        Debug.Log($"{GetType()}");
        await Awaitable.NextFrameAsync();
        gameLoop.ShowGameOverUi();
    }

    public override async Awaitable Doing()
    {
        while (!gameLoop.WantExit)
        {
            await Awaitable.WaitForSecondsAsync(0.5f);
        }
        //Debug.Log($"Is wana to exit {gameLoop.WantExit}");
    }

    public override async Awaitable Exit()
    {
        await Awaitable.NextFrameAsync();
    }
}