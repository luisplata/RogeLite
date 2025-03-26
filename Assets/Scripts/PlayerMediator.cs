using System;
using Bellseboss;
using TMPro;
using UnityEngine;

public class PlayerMediator : MonoBehaviour, IAttacker, IPlayerMediator
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Pistol pistol;
    [SerializeField] private PowerUpManager powerUpManager;
    [SerializeField] private Inventory inventory;
    [SerializeField] private TextMeshProUGUI stats, inventoryText;
    [SerializeField] private EquipmentManager equipmentManager;
    public PlayerStats PlayerStats => playerStats;
    public event Action<int> OnLevelUp;
    public event Action OnDie;
    public bool IsDead => playerStats.IsDead;

    public bool ICantGetMinerals { get; set; }

    private GameStateMachine _stateMachine;
    private bool _whileTrue;

    private Mineral currentMineral;

    private void OnEnable()
    {
        _whileTrue = true;
    }

    private void OnDisable()
    {
        _whileTrue = false;
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
            if (nextState == StateOfGame.EXIT)
            {
                break;
            }

            gameState = _stateMachine.GetState(nextState);
        }
    }

    public void DisableControls()
    {
        player.DisableControls();
    }

    public void CanGetMinerals(bool canGetMinerals, Mineral mineral)
    {
        ICantGetMinerals = canGetMinerals;
        currentMineral = mineral;
    }

    public void CanMove(bool canMove)
    {
        player.CanMove(canMove);
    }

    public void GetMinerals()
    {
        if (currentMineral == null) return;
        var loot = currentMineral as ILootSource;
        CollectLoot(loot.GetLoot());
        currentMineral.TryToDestroy();
    }

    public float GetTimeToMining()
    {
        return playerStats.GetTimeToMining();
    }

    public void Initialize()
    {
        player = GetComponent<Player>();
        playerStats = GetComponent<PlayerStats>();
        pistol = GetComponentInChildren<Pistol>();
        powerUpManager = GetComponent<PowerUpManager>();

        equipmentManager.Initialize(playerStats);
        player.Initialize(this);
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

    public void OnPowerUpApplied(PowerUp powerUp)
    {
        //Debug.Log($"Power-up aplicado: {powerUp.powerUpName}");
        powerUp.ApplyEffect(playerStats);
    }

    public void OnStatsChanged()
    {
        player.ApplyStats();
        pistol.UpdateStats(playerStats);
        stats.text = playerStats.GetFormattedStats() + "\n" + equipmentManager.GetFormattedEquipment();
        inventoryText.text = inventory.GetFormattedInventory();
    }

    public void GameOver()
    {
        if (isGameOverRunning) return;
        //Debug.Log(inventory.GetFormattedInventory());
        ServiceLocator.Instance.GetService<IDataBaseService>().SaveInventory(inventory);
        isGameOverRunning = true;
        OnDie?.Invoke();
    }

    private bool isGameOverRunning;

    public void OnKill(IDamageable target)
    {
        //Debug.Log($"Kill Enemy!");
        if (target is IXPSource xpSource)
        {
            GainXP(xpSource.GetXPAmount());
        }

        if (target is ILootSource lootSource)
        {
            CollectLoot(lootSource.GetLoot());
        }
    }

    private void GainXP(int amount)
    {
        playerStats.AddExp(amount);
        //Debug.Log($"Gained {amount} XP. Total XP: {playerStats.GetExp()}");
    }

    private void CollectLoot(Item[] loot)
    {
        foreach (var item in loot)
        {
            switch (item.Type)
            {
                case LootType.Equipable:
                case LootType.Consumable:
                case LootType.Mineral:
                    inventory.AddItem(item);
                    break;
                case LootType.Gold:
                    playerStats.AddGold(item.Stars);
                    break;
            }
            //Debug.Log($"Loot {item.Name} with {item.Stars} stars {playerStats.GetExp()}");
        }
    }

    public void LevelUp(int newLevel)
    {
        Debug.Log($"New Level! {newLevel}");
        //powerUpManager.ShowPowerUpOptions();
        OnLevelUp?.Invoke(newLevel);
    }

    public bool IsMining()
    {
        return ICantGetMinerals;
    }
}