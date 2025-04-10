using System.Collections.Generic;
using Items.Data;
using Items.Runtime;
using LootSystem;
using UnityEngine;

namespace Items.Factories
{
    public class LootItemFactory : ILootFactory
    {
        private readonly Dictionary<string, LootItem> _lootItemsByUUID;
        private ILootItemStarsFactory lootItemStarsFactory;

        public LootItemFactory(List<LootItem> lootItems)
        {
            _lootItemsByUUID = new Dictionary<string, LootItem>();
            foreach (var item in lootItems)
            {
                _lootItemsByUUID[item.UUID] = item;
            }

            lootItemStarsFactory = new WoWLootItemStarsFactory();
        }

        public LootItemInstance CreateLootItemInstance(LootItem lootItem, int stars, List<BaseStatOnItem> stats)
        {
            return new LootItemInstance(lootItem, stars) { GeneratedStats = stats };
        }

        public LootItemInstance CreateFromData(LootItemInstanceData data)
        {
            if (!_lootItemsByUUID.TryGetValue(data.LootItemUUID, out var lootItem))
            {
                Debug.LogError($"LootItem with UUID {data.LootItemUUID} not found!");
                return null;
            }

            return new LootItemInstance(data) { LootItemConfig = lootItem };
        }

        public List<LootItemInstance> GenerateLoot(LootTable lootTable, float luckFactor)
        {
            List<LootItemInstance> droppedItems = new();

            for (int i = 0; i < lootTable.MaxDrops; i++)
            {
                foreach (var entry in lootTable.LootEntries)
                {
                    float modifiedChance = entry.DropChance * luckFactor;

                    if (Random.value <= modifiedChance)
                    {
                        LootItemInstance newItem = lootItemStarsFactory.CreateLootItem(entry.Item);
                        droppedItems.Add(newItem);
                        break; // Este slot ya tiene un item, paso al siguiente
                    }
                }
            }

            Debug.Log($"LootTable: Dropped {droppedItems.Count} Items");
            return droppedItems;
        }
    }
}