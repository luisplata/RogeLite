using System;
using System.Collections.Generic;
using Items.Runtime;

namespace Inventory
{
    public interface IInventoryService
    {
        void AddItem(LootItemInstance item);
        List<LootItemInstance> GetAllItems();
        void LoadInventory();
        void SaveInventory();
        void SellItem(LootItemInstance itemInstance, int totalValue);
        
        event Action OnInventoryChanged;
    }
}