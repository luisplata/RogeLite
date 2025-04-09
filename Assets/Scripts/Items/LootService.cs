using System.Collections.Generic;
using Bellseboss;
using Items.Config;
using Items.Runtime;
using UnityEngine;

namespace Items
{
    public class LootService : ILootService
    {
        private readonly ILootItemFactory _lootItemFactory;
        private readonly LootItem _goldLootItem; // Se pasa como LootItem

        public LootService(ILootItemFactory lootItemFactory, LootItem goldLootItem)
        {
            _lootItemFactory = lootItemFactory;
            _goldLootItem = goldLootItem;
        }

        public List<LootItemInstance> GenerateLoot(LootTable lootTable, float luckFactor)
        {
            List<LootItemInstance> droppedItems = new();
            int drops = 0;

            // Generar loot de la tabla
            foreach (var entry in lootTable.lootEntries)
            {
                if (drops >= lootTable.maxDrops) break;
                float modifiedChance = entry.dropChance * luckFactor;
                if (Random.value <= modifiedChance)
                {
                    LootItemInstance newItem = _lootItemFactory.CreateLootItem(entry.item);
                    droppedItems.Add(newItem);
                    drops++;
                }
            }
            Debug.Log($"Dropped {droppedItems.Count} items");
            return droppedItems;
        }
    }

    public interface ILootService
    {
        List<LootItemInstance> GenerateLoot(LootTable lootTable, float luckFactor);
    }
}