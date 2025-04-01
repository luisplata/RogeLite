using System;
using Bellseboss;
using UnityEngine;

public class PlayerMediator : MonoBehaviour, IAttacker, IPlayerMediator
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Pistol pistol;
    [SerializeField] private PowerUpManager powerUpManager;
    [SerializeField] private Inventory inventory;
    [SerializeField] private EquipmentManager equipmentManager;
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

    public void DisableControls() => player.DisableControls();

    public void CanGetMinerals(bool canGetMinerals, Mineral mineral)
    {
        ICantGetMinerals = canGetMinerals;
        currentMineral = mineral;
    }

    public void CanMove(bool canMove) => player.CanMove(canMove);

    public void GetMinerals()
    {
        if (currentMineral == null) return;
        if (currentMineral is ILootSource lootSource)
        {
            CollectLoot(lootSource.GetLoot());
            currentMineral.TryToDestroy();
        }
    }

    public float GetTimeToMining() => playerStats.GetTimeToMining();
    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void Initialize(Joystick joystick)
    {
        equipmentManager.Initialize(playerStats);
        player.Initialize(this, joystick);
        playerStats.Initialize(this);
        pistol.Initialize(this, this);
        powerUpManager.Initialize(this);

        _stateMachine = new GameStateMachine();
        _stateMachine.AddInitialState(StateOfGame.INIT, new InitPlayerState(StateOfGame.PLAYER_WALK, this));
        _stateMachine.AddState(StateOfGame.PLAYER_WALK, new WalkPlayerState(StateOfGame.PLAYER_DEAD, this));
        _stateMachine.AddState(StateOfGame.PLAYER_SHOOT, new ShootingPlayerState(StateOfGame.PLAYER_DEAD, this));
        _stateMachine.AddState(StateOfGame.PLAYER_MINING, new MiningPlayerState(StateOfGame.PLAYER_DEAD, this));
        _stateMachine.AddState(StateOfGame.PLAYER_DEAD, new DeadPlayerState(StateOfGame.EXIT, this));

        _ = StartStateMachine();
    }

    public void OnPowerUpApplied(PowerUp powerUp) => powerUp.ApplyEffect(playerStats);

    public void OnStatsChanged()
    {
        player.ApplyStats();
        pistol.UpdateStats(playerStats);
        ServiceLocator.Instance.GetService<IUIGameScreen>()
            .SetStatsText(playerStats.GetFormattedStats() + "\n" + equipmentManager.GetFormattedEquipment());
        ServiceLocator.Instance.GetService<IUIGameScreen>().SetInventoryText(inventory.GetFormattedInventory());
    }

    public void GameOver()
    {
        if (isGameOverRunning) return;
        ServiceLocator.Instance.GetService<IDataBaseService>().SaveInventory(inventory);
        isGameOverRunning = true;
        OnDie?.Invoke();
    }

    public void OnKill(IDamageable target)
    {
        if (target is IXPSource xpSource) GainXP(xpSource.GetXPAmount());
        if (target is ILootSource lootSource) CollectLoot(lootSource.GetLoot());
    }

    private void GainXP(int amount) => playerStats.AddExp(amount);

    private void CollectLoot(LootItemInstance[] loot)
    {
        foreach (var item in loot)
        {
            switch (item.itemType)
            {
                case LootType.Equipable:
                case LootType.Consumable:
                case LootType.Mineral:
                    inventory.AddItem(item);
                    Debug.Log($"Added {item.itemName} ({item.stars}★) to inventory.");
                    break;
                case LootType.Gold:
                    var amount = item.stars;
                    playerStats.AddGold(amount);
                    Debug.Log($"Player received {amount} gold!");
                    break;
                default:
                    Debug.LogWarning($"Unknown loot type: {item.itemName}");
                    break;
            }
        }
    }


    public void LevelUp(int newLevel) => OnLevelUp?.Invoke(newLevel);
    public bool IsMining() => ICantGetMinerals;
    public void EquipItem(LootItemInstance item) => equipmentManager.EquipItem(item);
}