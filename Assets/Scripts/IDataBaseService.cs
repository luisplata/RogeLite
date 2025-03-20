using System.Collections.Generic;
using Bellseboss;

public interface IDataBaseService
{
    void SaveInventory(Inventory inventory);
    List<Item> GetItems();
}