using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bellseboss
{
    [Serializable]
    public class LootItemInstance : DroppedLoot
    {
        public LootItem LootItemData { get; }

        public LootItemInstance(LootItem lootItem, int stars)
            : base(lootItem.Data, stars)
        {
            LootItemData = lootItem;
        }

        public LootItemInstance(LootItemInstanceData data, LootItem lootItem)
            : base(lootItem.Data, data.stars)
        {
        }
    }
}