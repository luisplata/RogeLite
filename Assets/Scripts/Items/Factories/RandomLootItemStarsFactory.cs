using Items.Runtime;
using UnityEngine;

namespace Items.Factories
{
    public class RandomLootItemStarsFactory : ILootItemStarsFactory
    {
        public LootItemInstance CreateLootItem(LootItem baseItem)
        {
            int stars = Random.Range(1, 6);
            return new LootItemInstance(baseItem, stars);
        }
    }
}