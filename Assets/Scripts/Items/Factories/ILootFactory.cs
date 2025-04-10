using System.Collections.Generic;
using Items.Data;
using Items.Runtime;
using LootSystem;

namespace Items.Factories
{
    public interface ILootFactory
    {
        List<LootItemInstance> GenerateLoot(LootTable lootTable, float luckFactor);
        LootItemInstance CreateFromData(LootItemInstanceData data);
        LootItemInstance CreateLootItemInstance(LootItem lootItem, int stars, List<BaseStatOnItem> stats);
    }
}