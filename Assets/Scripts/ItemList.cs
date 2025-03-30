using System;
using System.Collections.Generic;

namespace Bellseboss
{
    [Serializable]
    public class ItemList
    {
        public List<LootItemInstance> Items = new();

        public ItemList()
        {
        }

        public ItemList(List<LootItemInstance> items) => Items = items;
    }
}