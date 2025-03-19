using Bellseboss;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMediator : MonoBehaviour, IAttacker
{
    public Player player;
    public PlayerStats playerStats;
    public Pistol pistol;
    public PowerUpManager powerUpManager;
    public Inventory inventory;
    [SerializeField] private TextMeshProUGUI stats, inventoryText;


    void Awake()
    {
        player = GetComponent<Player>();
        playerStats = GetComponent<PlayerStats>();
        pistol = GetComponentInChildren<Pistol>();
        powerUpManager = GetComponent<PowerUpManager>();

        // Configurar referencias en los componentes
        player.Initialize(this);
        playerStats.Initialize(this);
        pistol.Initialize(this, this);
        powerUpManager.Initialize(this);
    }

    public void OnPowerUpApplied(PowerUp powerUp)
    {
        Debug.Log($"Power-up aplicado: {powerUp.powerUpName}");
        powerUp.ApplyEffect(playerStats);
    }

    public void OnStatsChanged()
    {
        player.ApplyStats();
        pistol.UpdateStats(playerStats);
        stats.text = playerStats.GetFormattedStats();
        inventoryText.text = inventory.GetFormattedInventory();
    }

    public void GameOver()
    {
        SceneManager.LoadScene(0);
    }

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
        powerUpManager.ShowPowerUpOptions();
    }
}