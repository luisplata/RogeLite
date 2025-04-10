using System;
using UnityEngine;

public class PlayerMediator : MonoBehaviour, IAttacker, IPlayerMediator, IGraphicalCharacter
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Pistol pistol;
    [SerializeField] private PowerUpManager powerUpManager;
    [SerializeField] private EquipmentManager equipmentManager;
    [SerializeField] private GraphicalCharacter graphicalCharacter;
    public PlayerStats PlayerStats => playerStats;
    public event Action<int> OnLevelUp;
    public event Action OnDie;
    public bool IsDead => playerStats.IsDead;

    public bool ICantGetMinerals { get; set; }

    private GameStateMachine _stateMachine;
    private bool _whileTrue;

    private Mineral currentMineral;
    private bool isGameOverRunning;

    private void OnEnable() => _whileTrue = true;
    private void OnDisable() => _whileTrue = false;

    public void Initialize(Joystick joystick)
    {
        equipmentManager.Initialize(playerStats);
        player.Initialize(this, joystick);
        playerStats.Initialize(this);
        pistol.Initialize(this, this);
        powerUpManager.Initialize(this);
        graphicalCharacter.Initialize(this);

        _stateMachine = new GameStateMachine();
        _stateMachine.AddInitialState(StateOfGame.INIT, new InitPlayerState(StateOfGame.PLAYER_WALK, this));
        _stateMachine.AddState(StateOfGame.PLAYER_WALK, new WalkPlayerState(StateOfGame.PLAYER_DEAD, this));
        _stateMachine.AddState(StateOfGame.PLAYER_SHOOT, new ShootingPlayerState(StateOfGame.PLAYER_DEAD, this));
        _stateMachine.AddState(StateOfGame.PLAYER_MINING, new MiningPlayerState(StateOfGame.PLAYER_DEAD, this));
        _stateMachine.AddState(StateOfGame.PLAYER_DEAD, new DeadPlayerState(StateOfGame.EXIT, this));

        _ = StartStateMachine();
    }

    private async Awaitable StartStateMachine()
    {
        var gameState = _stateMachine.GetInitialState();
        while (_whileTrue)
        {
            await gameState.Enter();
            await gameState.Doing();
            await gameState.Exit();
            var nextState = gameState.NextState();
            if (nextState == StateOfGame.EXIT) break;
            gameState = _stateMachine.GetState(nextState);
        }
    }

    public void DisableControls()
    {
        player.DisableControls();
        pistol.DisableControls();
    }

    public void CanGetMinerals(bool canGetMinerals, Mineral mineral)
    {
        ICantGetMinerals = canGetMinerals;
        currentMineral = mineral;
    }

    public void CanMove(bool canMove) => player.CanMove(canMove);

    public void GetMinerals()
    {
        if (currentMineral == null) return;
        currentMineral.TryToDestroy();
    }

    public float GetTimeToMining() => playerStats.GetTimeToMining();

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void OnPowerUpApplied(PowerUp powerUp) => powerUp.ApplyEffect(playerStats);

    public void OnStatsChanged()
    {
        player.ApplyStats();
        pistol.UpdateStats(playerStats);
        ServiceLocator.Instance.GetService<IUIGameScreen>()
            .SetStatsText(playerStats.GetFormattedStats());
    }

    public void GameOver()
    {
        if (isGameOverRunning) return;
        isGameOverRunning = true;
        OnDie?.Invoke();
    }

    public void OnKill(IDamageable target)
    {
        if (target is IXPSource xpSource) GainXP(xpSource.GetXPAmount());
    }

    private void GainXP(int amount) => playerStats.AddExp(amount);


    public void LevelUp(int newLevel) => OnLevelUp?.Invoke(newLevel);
    public bool IsMining() => ICantGetMinerals;
}