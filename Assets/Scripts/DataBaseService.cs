using System.Collections.Generic;
using UnityEngine;
using Bellseboss;
using Bellseboss.Items;
using Items;
using Items.Config;
using Items.Data;
using Items.Runtime;

public class DataBaseService : MonoBehaviour, IDataBaseService
{
    [SerializeField] private List<LootItem> lootItems;

    private IDataPersistenceService _dataPersistenceService;
    private IInventoryService _inventoryService;
    private IGoldService _goldService;

    private void Awake()
    {
        if (FindObjectsByType<DataBaseService>(FindObjectsSortMode.None).Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        _dataPersistenceService = new PlayerPrefsDataPersistenceService();
        _inventoryService = new InventoryService(_dataPersistenceService, lootItems);
        _goldService = new GoldService(_dataPersistenceService);

        ServiceLocator.Instance.RegisterService(_dataPersistenceService);
        ServiceLocator.Instance.RegisterService(_inventoryService);
        ServiceLocator.Instance.RegisterService(_goldService);
        ServiceLocator.Instance.RegisterService<IDataBaseService>(this);
        ServiceLocator.Instance.RegisterService<IPlayerConfigurationService>(
            new PlayerConfigurationService(_dataPersistenceService, _inventoryService, this));

        DontDestroyOnLoad(gameObject);
    }

    public void AddItem(LootItemInstance item)
    {
        _inventoryService.AddItem(item);
    }

    public List<LootItemInstance> GetItems()
    {
        return _inventoryService.GetItems();
    }

    public List<LootItem> GetListItemLoot()
    {
        return lootItems;
    }

    public void ClearItems()
    {
        _inventoryService.ClearInventory();
    }

    public void SaveInventory(Inventory inventory)
    {
        foreach (var item in inventory.GetAllItems())
        {
            _inventoryService.AddItem(item);
        }
    }

}