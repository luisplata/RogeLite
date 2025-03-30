using System;
using System.Collections.Generic;

namespace Bellseboss
{
    [Serializable]
    public class LootItemInstanceData
    {
        public string itemName;
        public LootType itemType;
        public int stars;
        public EquipmentSlot slot;
        public List<BaseStatsOnItem> stats;

        public LootItemInstanceData(LootItemInstance instance)
        {
            itemName = instance.itemName;
            itemType = instance.itemType;
            stars = instance.stars;
            slot = instance.Slot;
            stats = instance.stats ?? new List<BaseStatsOnItem>();
        }

        public LootItemInstance ToLootItemInstance(LootItem lootItem)
        {
            return new LootItemInstance(lootItem, stars);
        }
    }
}