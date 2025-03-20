using System;
using Bellseboss;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void DisableControls()
    {
        player.DisableControls();
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
            if (item.Type == LootType.Equipable)
            {
                inventory.AddItem(item);
            }
            else if (item.Type == LootType.Consumable)
            {
                Debug.Log("Consume");
            }
            else if (item.Type == LootType.Gold)
            {
                playerStats.AddGold(item.Stars);
            }
            else if (item.Type == LootType.Mineral)
            {
                Debug.Log("Mineral");
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
}