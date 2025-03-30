using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bellseboss
{
    [Serializable]
    public class LootItemInstance : DroppedLoot
    {
        public LootItemInstance(LootItem lootItem, int stars) 
            : base(lootItem.itemName, lootItem.lootType, stars, lootItem.equipmentSlot, lootItem.itemSprite, GenerateStats(lootItem, stars))
        {
        }

        public LootItemInstance(LootItemInstanceData data, LootItem lootItem)
            : base(data.itemName, data.itemType, data.stars, data.slot, lootItem.itemSprite, data.stats)
        {
        }

        private static List<BaseStatsOnItem> GenerateStats(LootItem lootItem, int starsFromItem)
        {
            List<BaseStatsOnItem> generatedStats = new();
            foreach (var baseStat in lootItem.baseStatsOnItem)
            {
                generatedStats.Add(new BaseStatsOnItem(baseStat.statType, baseStat.statValue * starsFromItem));
            }
            return generatedStats;
        }
    }
}