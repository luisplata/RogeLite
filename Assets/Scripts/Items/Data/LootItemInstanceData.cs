using System;
using System.Collections.Generic;
using Items.Runtime;

namespace Items.Data
{
    [Serializable]
    public class LootItemInstanceData
    {
        public string InstanceUUID;
        public string LootItemUUID;
        public int Stars;
        public List<BaseStatOnItem> GeneratedStats = new();

        public LootItemInstanceData(LootItemInstance instance)
        {
            InstanceUUID = instance.InstanceUUID;
            LootItemUUID = instance.LootItemUUID;
            Stars = instance.Stars;
            GeneratedStats = instance.GeneratedStats;
        }

        public LootItemInstance ToLootItemInstance(LootItem lootItem)
        {
            var instance = new LootItemInstance(this);
            instance.LootItemConfig = lootItem;
            return instance;
        }
    }
}