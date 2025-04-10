using System.Collections.Generic;
using Items.Runtime;
using UnityEngine;

namespace LootSystem
{
    public class LootGenerator
    {
        public static List<LootItemInstance> GenerateLoot(LootTable lootTable)
        {
            List<LootItemInstance> drops = new();

            foreach (var entry in lootTable.LootEntries)
            {
                if (Random.value <= entry.DropChance / 100f)
                {
                    int stars = Random.Range(entry.MinStars, entry.MaxStars + 1);
                    var itemInstance = new LootItemInstance(entry.Item, stars);
                    drops.Add(itemInstance);
                }
            }

            // Limitar los drops al máximo permitido
            if (drops.Count > lootTable.MaxDrops)
            {
                drops = GetRandomSubset(drops, lootTable.MaxDrops);
            }

            return drops;
        }

        private static List<LootItemInstance> GetRandomSubset(List<LootItemInstance> list, int count)
        {
            List<LootItemInstance> result = new();
            while (result.Count < count)
            {
                var randomItem = list[Random.Range(0, list.Count)];
                if (!result.Contains(randomItem))
                    result.Add(randomItem);
            }
            return result;
        }
    }
}