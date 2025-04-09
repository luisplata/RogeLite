using System.Collections.Generic;
using Bellseboss;
using Bellseboss.Items;
using Items;
using Items.Config;
using Items.Runtime;

public interface IDataBaseService
{
    void SaveInventory(Inventory inventory);
    List<LootItemInstance> GetItems();
    List<LootItem> GetListItemLoot();
}