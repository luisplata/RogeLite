using System.Collections.Generic;
using Bellseboss;

public interface ILootFactory
{
    List<Item> GenerateLoot(LootTable lootTable, float luckFactor);
}