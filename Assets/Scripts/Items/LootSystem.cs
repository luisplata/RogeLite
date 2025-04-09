using System.Collections.Generic;
using Bellseboss;
using Items.Config;
using Items.Runtime;
using UnityEngine;

namespace Items
{
    public class LootSystem
    {
        public List<LootItem> GetLootItems(LootTable lootTable, float luckFactor)
        {
            List<LootItem> selectedLootItems = new();
            int drops = 0;

            foreach (var entry in lootTable.lootEntries)
            {
                if (drops >= lootTable.maxDrops) break;

                float modifiedChance = entry.dropChance * luckFactor;
                if (Random.value <= modifiedChance)
                {
                    selectedLootItems.Add(entry.item);
                    drops++;
                }
            }

            return selectedLootItems;
        }

        public List<LootItemInstance> ConvertLootItemsToInstances(List<LootItem> lootItems)
        {
            List<LootItemInstance> droppedItems = new();

            foreach (var lootItem in lootItems)
            {
                LootItemInstance newItem = ItemBuilder.Create(lootItem);
                droppedItems.Add(newItem);
            }

            return droppedItems;
        }
    }
}