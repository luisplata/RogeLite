using System;
using System.Collections.Generic;
using Bellseboss;
using Items.Data;

namespace Items
{
    [Serializable]
    public class InventoryData
    {
        public List<LootItemInstanceData> Items = new();
    }
}