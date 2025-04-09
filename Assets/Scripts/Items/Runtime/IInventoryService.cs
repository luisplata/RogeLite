using System;
using System.Collections.Generic;

namespace Items.Runtime
{
    public interface IInventoryService
    {
        void AddItem(LootItemInstance item);
        void RemoveItem(LootItemInstance item);
        void SellItem(LootItemInstance item, int price);
        void BuyItem(LootItemInstance item, int price);
        List<LootItemInstance> GetItems();
        void ClearInventory();
        event Action OnInventoryChanged;
    }
}