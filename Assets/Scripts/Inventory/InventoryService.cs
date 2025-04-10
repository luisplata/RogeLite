using System;
using System.Collections.Generic;
using Items;
using Items.Data;
using Items.Factories;
using Items.Runtime;

namespace Inventory
{
    public class InventoryService : IInventoryService
    {
        private const string InventoryKey = "player_inventory"; // Clave para guardar en PlayerPrefs
        private IDataPersistenceService _dataPersistenceService;

        private List<LootItemInstance> _inventoryItems = new List<LootItemInstance>();
        private readonly ILootFactory _lootFactory;

        // Constructor
        public InventoryService(IDataPersistenceService dataPersistenceService, ILootFactory lootFactory)
        {
            _dataPersistenceService = dataPersistenceService;
            _lootFactory = lootFactory;
            LoadInventory();
        }

        public void AddItem(LootItemInstance item)
        {
            _inventoryItems.Add(item);
            SaveInventory();
        }

        public List<LootItemInstance> GetAllItems()
        {
            return _inventoryItems;
        }

        public void LoadInventory()
        {
            var inventoryData = _dataPersistenceService.Load(InventoryKey, new InventoryData());
            _inventoryItems.Clear();

            foreach (var itemData in inventoryData.Items)
            {
                _inventoryItems.Add(_lootFactory.CreateFromData(itemData));
            }
        }

        public void SaveInventory()
        {
            var inventoryData = new InventoryData();
            foreach (var item in _inventoryItems)
            {
                var itemData = new LootItemInstanceData(item);
                inventoryData.Items.Add(itemData);
            }
            _dataPersistenceService.Save(InventoryKey, inventoryData);
            OnInventoryChanged?.Invoke();
        }

        public void SellItem(LootItemInstance itemInstance, int totalValue)
        {
            // Remove the item from the inventory
            _inventoryItems.Remove(itemInstance);
            SaveInventory();
        }

        public event Action OnInventoryChanged;
    }
    
    [Serializable]
    public class InventoryData
    {
        public List<LootItemInstanceData> Items = new();
    }
}
