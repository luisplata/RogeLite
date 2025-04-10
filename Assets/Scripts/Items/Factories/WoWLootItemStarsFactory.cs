using Items.Runtime;
using UnityEngine;

namespace Items.Factories
{
    public class WoWLootItemStarsFactory : ILootItemStarsFactory
    {
        public LootItemInstance CreateLootItem(LootItem baseItem)
        {
            int stars = GetStarFromProbability();

            return new LootItemInstance(baseItem, stars);
        }

        private int GetStarFromProbability()
        {
            float roll = Random.value * 100f; // Random entre 0 y 100
        
            if (roll <= 50f) // 1 estrella - 50%
                return 1;
            else if (roll <= 75f) // 2 estrellas - 25%
                return 2;
            else if (roll <= 90f) // 3 estrellas - 15%
                return 3;
            else if (roll <= 97f) // 4 estrellas - 7%
                return 4;
            else // 5 estrellas - 3%
                return 5;
        }
    }
}