using System.Collections.Generic;
using Items.Runtime;

namespace Items
{
    public interface ILootable
    {
        List<LootItemInstance> GetLoot();
    }
    
    public interface IGoldLootable
    {
        int GetGold();
    }
}
