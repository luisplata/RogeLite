using System;
using System.Collections.Generic;
using Items.Runtime;

namespace Items
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