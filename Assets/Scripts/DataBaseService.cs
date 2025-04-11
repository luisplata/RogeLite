using System.Collections.Generic;
using City;
using Inventory;
using Items;
using Items.Equipment;
using Items.Factories;
using UnityEngine;

public class DataBaseService : MonoBehaviour, IDataBaseService
{
    [SerializeField] private List<LootItem> lootItems;
    private IDataPersistenceService _dataPersistenceService;
    private ILootFactory _factoryItems;
    private IInventoryService _inventoryService;
    private IPlayerGoldPersistenceService _playerGoldPersistenceService;
    private IGoldGenerationService _goldGenerationService;
    private IEquipmentPersistenceService _equipmentPersistenceService;
    private IMapPersistenceService _mapPersistenceService; 

    private void Awake()
    {
        if (FindObjectsByType<DataBaseService>(FindObjectsSortMode.None).Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        _dataPersistenceService = new PlayerPrefsDataPersistenceService();
        _factoryItems = new LootItemFactory(lootItems);
        _inventoryService = new InventoryService(_dataPersistenceService, _factoryItems);
        _playerGoldPersistenceService = new PlayerGoldPersistenceService(_dataPersistenceService);
        _goldGenerationService = new GoldGenerationService();
        _equipmentPersistenceService = new EquipmentPersistenceService(
            _dataPersistenceService,
            _factoryItems,
            _inventoryService
        );
        _mapPersistenceService = new MapPersistenceService(_dataPersistenceService);

        ServiceLocator.Instance.RegisterService(_dataPersistenceService);
        ServiceLocator.Instance.RegisterService(_factoryItems);
        ServiceLocator.Instance.RegisterService<IDataBaseService>(this);
        ServiceLocator.Instance.RegisterService(_inventoryService);
        ServiceLocator.Instance.RegisterService(_playerGoldPersistenceService);
        ServiceLocator.Instance.RegisterService(_goldGenerationService);
        ServiceLocator.Instance.RegisterService(_equipmentPersistenceService);
        ServiceLocator.Instance.RegisterService(_mapPersistenceService);
        ServiceLocator.Instance.RegisterService<IPlayerConfigurationService>(
            new PlayerConfigurationService(_dataPersistenceService, _inventoryService, _equipmentPersistenceService));

        DontDestroyOnLoad(gameObject);
    }
}