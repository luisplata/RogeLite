using System;
using System.Collections.Generic;

namespace Bellseboss
{
    [Serializable]
    public class LootItemInstanceData
    {
        public LootItem lootItemId;
        public int stars;
        public List<BaseStatsOnItem> stats;

        public LootItemInstanceData(LootItemInstance instance)
        {
            lootItemId = instance.LootItemData;
            stars = instance.stars;
            stats = instance.Data.baseStats;
        }

        public LootItemInstance ToLootItemInstance(LootItem lootItem)
        {
            return new LootItemInstance(lootItem, stars);
        }
    }


}