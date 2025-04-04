﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLootItem", menuName = "Loot System/Loot Item")]
public class LootItem : ScriptableObject
{
    public string itemName;
    public LootType lootType;
    public Sprite itemSprite;
    public EquipmentSlot equipmentSlot; // Nuevo: Define en qué slot se puede equipar

    public List<BaseStatsOnItem> baseStatsOnItem = new()
    {
        new(StatType.Attack, 0),
        new(StatType.Defense, 0),
        new(StatType.Speed, 0),
        new(StatType.CooldownReduction, 0),
        new(StatType.Heal, 0),
        new(StatType.AttackSpeed, 0)
    };

    private void OnEnable()
    {
        InitializeBaseStats();
    }

    private void InitializeBaseStats()
    {
        if (baseStatsOnItem.Count == 0)
        {
            baseStatsOnItem = new List<BaseStatsOnItem>
            {
                new(StatType.Attack, 0),
                new(StatType.Defense, 0),
                new(StatType.Speed, 0),
                new(StatType.CooldownReduction, 0),
                new(StatType.Heal, 0),
                new(StatType.AttackSpeed, 0)
            };
        }
    }
}