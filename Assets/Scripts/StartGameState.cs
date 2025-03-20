using UnityEngine;

public class StartGameState : StateBase
{
    public StartGameState(StateOfGame next, IGameLoop mediator) : base(next, mediator)
    {
    }

    public override async Awaitable Enter()
    {
        Debug.Log($"{GetType()}");
        gameLoop.EnablePlayerMovement();
        gameLoop.EnableSpawnEnemies();
        gameLoop.HideMenuUI();
        await Awaitable.NextFrameAsync();
    }

    public override async Awaitable Doing()
    {
        while (gameLoop.StartGame && !gameLoop.PlayerIsDead)
        {
            await Awaitable.WaitForSecondsAsync(0.5f);
        }
    }

    public override async Awaitable Exit()
    {
        gameLoop.DisablePlayerMovement();
        gameLoop.DisableSpawnEnemies();
        await Awaitable.NextFrameAsync();
    }
}