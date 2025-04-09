using System.Collections.Generic;
using Bellseboss;
using Bellseboss.Items;
using Items.Config;
using Items.Runtime;

namespace Items
{
    public interface ILootFactory
    {
        List<LootItemInstance> GenerateLoot(LootTable lootTable, float luckFactor);
        LootItemInstance CreateLootItem(LootItem lootItem);
        LootItemInstance GenerateGold(float luckFactor);
    }
}