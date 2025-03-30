using System.Collections.Generic;
using Bellseboss;

public interface ILootFactory
{
    List<LootItemInstance> GenerateLoot(LootTable lootTable, float luckFactor);
    LootItemInstance CreateLootItem(LootItem lootItem);
    LootItemInstance GenerateGold(float luckFactor);
}