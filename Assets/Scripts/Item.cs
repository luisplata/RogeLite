using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Bellseboss
{
    [Serializable]
    public class Item
    {
        public string itemName;
        public LootType itemType;
        public int stars;
        public List<BaseStatsOnItem> stats;
        public EquipmentSlot? Slot { get; } // Nuevo campo opcional

        public Item(string name, LootType type, int stars, EquipmentSlot? slot = null)
        {
            itemName = name;
            itemType = type;
            this.stars = stars;
            Slot = slot;
        }

        public bool IsEquipable() => Slot != null;
    }

    [Serializable]
    public class InventoryData
    {
        public List<Item> Items = new();
    }
}