using Items.Config;
using Items.Runtime;
using UnityEngine;

namespace Items
{
    public class RandomLootItemFactory : ILootItemFactory
    {
        public LootItemInstance CreateLootItem(LootItem baseItem)
        {
            int stars = Random.Range(1, 6);
            return new LootItemInstance(baseItem, stars);
        }
    }
}