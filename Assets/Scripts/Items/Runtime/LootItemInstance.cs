using System;
using System.Collections.Generic;
using Items;
using Items.Data;
using UnityEngine;

namespace Items.Runtime
{
    [Serializable]
    public class LootItemInstance
    {
        public string InstanceUUID { get; private set; }
        public string LootItemUUID { get; private set; } // Reference to LootItem config
        public int Stars { get; set; }
        public List<BaseStatOnItem> GeneratedStats { get; set; } = new();
        public WeaponType WeaponType => LootItemConfig.WeaponType;

        [NonSerialized]
        public LootItem LootItemConfig; // Loaded at runtime from UUID

        public LootItemInstance(LootItem lootItem, int stars)
        {
            InstanceUUID = Guid.NewGuid().ToString();
            LootItemUUID = lootItem.UUID;
            LootItemConfig = lootItem;
            Stars = stars;
            GeneratedStats = StatGenerator.GenerateStats(lootItem, stars);
        }

        public LootItemInstance(LootItemInstanceData data)
        {
            InstanceUUID = data.InstanceUUID;
            LootItemUUID = data.LootItemUUID;
            Stars = data.Stars;
            GeneratedStats = data.GeneratedStats;
        }

        public bool IsEquipable() => LootItemConfig.EquipmentSlot != EquipmentSlot.None;
    }
}

namespace LootSystem
{
    [Serializable]
    public class LootEntry
    {
        public LootItem Item;
        [Range(0, 100)] public float DropChance; // Probabilidad del item (%)
        public int MinStars = 1;
        public int MaxStars = 5;
    }
}
