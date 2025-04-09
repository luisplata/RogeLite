using UnityEngine;

namespace Bellseboss
{
    public class CustomLootItemFactory : ILootItemFactory
    {
        private readonly IStatModifierStrategy _statModifierStrategy;

        public CustomLootItemFactory(IStatModifierStrategy statModifierStrategy)
        {
            _statModifierStrategy = statModifierStrategy;
        }

        public LootItemInstance CreateLootItem(LootItem baseItem)
        {
            int stars = Random.Range(1, 6);
            var stats = _statModifierStrategy.ModifyStats(baseItem.Data.baseStats, stars);
            return new LootItemInstance(baseItem, stars) { Data = { baseStats = stats } };
        }
    }
}