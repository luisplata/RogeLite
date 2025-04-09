using System;
using System.Collections.Generic;
using Items.Config;
using Items.Data;
using UnityEngine;

namespace Items.Runtime
{
    public class InventoryService : IInventoryService
    {
        private readonly IDataPersistenceService _dataPersistenceService;
        private readonly List<LootItem> _lootItems;
        private const string InventoryKey = "Inventory";

        public event Action OnInventoryChanged;

        public InventoryService(IDataPersistenceService dataPersistenceService, List<LootItem> lootItems)
        {
            _dataPersistenceService = dataPersistenceService;
            _lootItems = lootItems;
        }

        public void AddItem(LootItemInstance item)
        {
            InventoryData inventory = LoadInventory();
            inventory.Items.Add(new LootItemInstanceData(item));
            SaveInventory(inventory);
        }
        
        
        private void SaveInventory(List<LootItemInstance> instances)
        {
            List<LootItemInstanceData> dataList = new List<LootItemInstanceData>();

            foreach (var instance in instances)
            {
                dataList.Add(new LootItemInstanceData(instance));  // Convertimos cada LootItemInstance en LootItemInstanceData
            }

            _dataPersistenceService.Save(InventoryKey, dataList);  // Guardamos la lista de datos
        }

        public void RemoveItem(LootItemInstance item)
        {
            InventoryData inventory = LoadInventory();
            Debug.Log($"item.UUID {item.UUID}");
            Debug.Log($"item.LootItemData.Data.UUID {item.Data.UUID}");
            var itemToRemove = inventory.Items.Find(x => x.LootItemUUID == item.UUID);

            if (itemToRemove != null)
            {
                inventory.Items.Remove(itemToRemove);
            }

            SaveInventory(inventory);
        }

        public void SellItem(LootItemInstance item, int price)
        {
            RemoveItem(item);
            // Dar dinero al jugador...
        }

        public void BuyItem(LootItemInstance item, int price)
        {
            // Cobrar dinero al jugador...
            AddItem(item);
        }

        public List<LootItemInstance> GetItems()
        {
            InventoryData inventory = LoadInventory();
            List<LootItemInstance> items = new();

            foreach (var itemData in inventory.Items)
            {
                LootItem lootItem = _lootItems.Find(item => item.Data.itemName == itemData.LootItemUUID);
                if (lootItem != null)
                    items.Add(new LootItemInstance(lootItem, itemData.Stars));
            }

            return items;
        }

        public void ClearInventory()
        {
            _dataPersistenceService.Delete(InventoryKey);
        }

        private InventoryData LoadInventory()
        {
            return _dataPersistenceService.Load(InventoryKey, new InventoryData());
        }
        
        private List<LootItemInstance> LoadInventoryy()
        {
            List<LootItemInstanceData> loadedData = _dataPersistenceService.Load(InventoryKey, new List<LootItemInstanceData>());

            List<LootItemInstance> instances = new List<LootItemInstance>();

            foreach (var data in loadedData)
            {
                LootItem lootItem = _lootItems.Find(item => item.Data.itemName == data.LootItemName);
                LootItemInstance instance = new LootItemInstance(data, lootItem);
                instances.Add(instance);
            }

            return instances;
        }


        private void SaveInventory(InventoryData inventory)
        {
            _dataPersistenceService.Save(InventoryKey, inventory);
            OnInventoryChanged?.Invoke();
        }
    }
}