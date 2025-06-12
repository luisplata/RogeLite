using System.Collections.Generic;
using Inventory;
using Items;
using Items.Factories;
using Items.Runtime;
using LootSystem;
using UnityEngine;

public class SlimeEnemyMediator : SlimeMediator, ILootable, IGoldLootable, IXPSource, IEnemy
{
    [SerializeField] private LootTable lootTable;
    [SerializeField] private float luckFactor = 1.0f; //TODO get luck factor from player
    [SerializeField] private int goldBase = 1;
    [SerializeField] private int xp = 1;

    public List<LootItemInstance> GetLoot()
    {
        var lootItems = ServiceLocator.Instance.GetService<ILootFactory>()
            .GenerateLoot(lootTable, luckFactor);
        return lootItems;
    }

    public int GetGold()
    {
        return ServiceLocator.Instance.GetService<IGoldGenerationService>()
            .GenerateGold(goldBase, luckFactor, slimeStats.Level);
    }

    public int GetXPAmount()
    {
        return xp * slimeStats.Level;
    }

    public LootItemInstance GetEquippedWeaponInstance()
    {
        return equippedWeaponInstance;
    }

    public int CurrentHp => (int)slimeStats.CurrentHp;
}

public interface IEnemy
{
    LootItemInstance GetEquippedWeaponInstance();
    string SlimeName { get; }
    int CurrentHp { get; }
}