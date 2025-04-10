using System.Collections.Generic;
using Items.Runtime;
using LootSystem;

namespace Items.Factories
{
    public interface IFactoryItems
    {
        List<LootItemInstance> GenerateLoot(LootTable lootTable, float luckFactor);
    }
}