using System.Collections.Generic;
using System.Linq;
using Inventory;
using Items.Data;
using Items.Factories;
using Items.Runtime;

namespace Items.Equipment
{
    public class EquipmentPersistenceService : IEquipmentPersistenceService
    {
        private const string EQUIPPED_ITEMS_KEY = "EquippedItems";
        private readonly IDataPersistenceService _dataPersistenceService;
        private readonly ILootFactory _lootFactory;
        private List<LootItemInstance> _inventoryItems = new();
        private readonly IInventoryService _inventoryService;

        public EquipmentPersistenceService(
            IDataPersistenceService dataPersistenceService,
            ILootFactory lootFactory,
            IInventoryService inventoryService
        )
        {
            _dataPersistenceService = dataPersistenceService;
            _lootFactory = lootFactory;
            _inventoryService = inventoryService;
            LoadEquipment();
        }

        public void SaveEquippedItems(List<LootItemInstance> equippedItems)
        {
            var inventoryData = new InventoryData();
            foreach (var item in _inventoryItems)
            {
                var itemData = new LootItemInstanceData(item);
                inventoryData.Items.Add(itemData);
            }

            _dataPersistenceService.Save(EQUIPPED_ITEMS_KEY, inventoryData);
        }

        public void LoadEquipment()
        {
            var inventoryData = _dataPersistenceService.Load(EQUIPPED_ITEMS_KEY, new InventoryData());
            _inventoryItems.Clear();

            foreach (var itemData in inventoryData.Items)
            {
                _inventoryItems.Add(_lootFactory.CreateFromData(itemData));
            }
        }

        public List<LootItemInstance> LoadEquippedItems()
        {
            var data = _dataPersistenceService.Load(EQUIPPED_ITEMS_KEY, new InventoryData());
            return data.Items.Select(itemData => _lootFactory.CreateFromData(itemData)).ToList();
        }

        public LootItemInstance GetEquippedItem(EquipmentSlot slot)
        {
            return _inventoryItems.FirstOrDefault(x => x.LootItemConfig.EquipmentSlot == slot);
        }

        public void EquipItem(LootItemInstance item)
        {
            var existingItem = _inventoryItems
                .FirstOrDefault(x => x.LootItemConfig.EquipmentSlot == item.LootItemConfig.EquipmentSlot);
            if (existingItem != null)
            {
                _inventoryService.AddItem(existingItem);
                _inventoryItems.Remove(existingItem);
            }

            _inventoryItems.Add(item);
            SaveEquippedItems(_inventoryItems);
        }
    }
}