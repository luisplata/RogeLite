using System;
using System.Collections.Generic;
using Items.Config;
using Items.Runtime;

namespace Items.Data
{
    [Serializable]
    public class LootItemInstanceData
    {
        public string LootItemUUID;  // Solo el UUID del LootItem
        public int Stars;
        public List<BaseStatsOnItem> BaseStats;
        public string LootItemName;

        public LootItemInstanceData(LootItemInstance instance)
        {
            LootItemUUID = instance.Data.UUID;
            LootItemName = instance.Data.itemName;
            Stars = instance.Stars;
            BaseStats = instance.BaseStats;
        }

        public LootItemInstance ToLootItemInstance(LootItem lootItem)
        {
            return new LootItemInstance(lootItem, Stars);
        }
    }
}

namespace Items.Runtime
{
}