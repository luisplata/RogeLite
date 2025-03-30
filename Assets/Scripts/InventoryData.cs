using System;
using System.Collections.Generic;

namespace Bellseboss
{
    [Serializable]
    public class InventoryData
    {
        public List<LootItemInstanceData> Items = new();
    }
}