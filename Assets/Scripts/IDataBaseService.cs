using System.Collections.Generic;
using Bellseboss;

public interface IDataBaseService
{
    void SaveInventory(Inventory inventory);
    List<LootItemInstance> GetItems();
    List<LootItem> GetListItemLoot();
}