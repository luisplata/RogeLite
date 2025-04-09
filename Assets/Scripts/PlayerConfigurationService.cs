using UnityEngine;
using System.Collections.Generic;
using Bellseboss;
using Bellseboss.Items;
using Items;
using Items.Config;
using Items.Data;
using Items.Runtime;

public class PlayerConfigurationService : IPlayerConfigurationService
{
    private const string CHARACTER_TYPE_KEY = "CharacterType";
    private const string EQUIPPED_ITEMS_KEY = "EquippedItems";
    private const string PLAYER_STATS_KEY = "PlayerStats";

    private CharacterType _characterType;
    private Dictionary<EquipmentSlot, LootItemInstance> _equippedItems = new();
    private PlayerGlobalStats _stats = new();
    private readonly IDataPersistenceService _dataPersistenceService;
    private readonly IInventoryService _inventoryService;
    private readonly IDataBaseService _dataBaseService;

    public PlayerConfigurationService(IDataPersistenceService dataPersistenceService,
        IInventoryService inventoryService, IDataBaseService dataBaseService)
    {
        _dataPersistenceService = dataPersistenceService;
        _inventoryService = inventoryService;
        _dataBaseService = dataBaseService;
        LoadFromPrefs();
    }

    public void SetCharacterType(CharacterType type)
    {
        _characterType = type;
        PlayerPrefs.SetInt(CHARACTER_TYPE_KEY, (int)type);
        PlayerPrefs.Save();
    }

    public CharacterType GetCharacterType() => _characterType;

    public void EquipItem(LootItemInstance item)
    {
        if (_equippedItems.ContainsKey(item.Data.equipmentSlot) && _equippedItems[item.Data.equipmentSlot] != null)
        {
            _inventoryService.AddItem(_equippedItems[item.Data.equipmentSlot]);
        }

        _equippedItems[item.Data.equipmentSlot] = item;
        SaveEquippedItems();
        _inventoryService.RemoveItem(item);
    }

    public LootItemInstance GetEquippedItem(EquipmentSlot slot)
    {
        return _equippedItems.GetValueOrDefault(slot);
    }

    public void SetStats(PlayerGlobalStats stats)
    {
        _stats = stats;
        SaveStats();
    }

    public PlayerGlobalStats GetStats() => _stats;

    private void LoadFromPrefs()
    {
        _characterType = (CharacterType)PlayerPrefs.GetInt(CHARACTER_TYPE_KEY, (int)CharacterType.Human);
        _equippedItems = LoadEquippedItems();
        _stats = LoadStats();
    }

    private void SaveEquippedItems()
    {
        EquippedItemsData equippedData = new EquippedItemsData();

        foreach (var item in _equippedItems.Values)
        {
            equippedData.Items.Add(new LootItemInstanceData(item));
        }

        _dataPersistenceService.Save(EQUIPPED_ITEMS_KEY, equippedData);
        // string json = JsonUtility.ToJson(equippedData);
        // Debug.Log($"{EQUIPPED_ITEMS_KEY} Saving equipped items: {json}");
        // PlayerPrefs.SetString(EQUIPPED_ITEMS_KEY, json);
        // PlayerPrefs.Save();
    }

    private Dictionary<EquipmentSlot, LootItemInstance> LoadEquippedItems()
    {
        EquippedItemsData equippedData = _dataPersistenceService.Load(EQUIPPED_ITEMS_KEY, new EquippedItemsData());
        Dictionary<EquipmentSlot, LootItemInstance> equippedItems = new();

        foreach (var itemData in equippedData.Items)
        {
            LootItem lootItem = FindLootItemByName(itemData.LootItemUUID);
            if (lootItem != null)
            {
                equippedItems[itemData.ToLootItemInstance(lootItem).Data.equipmentSlot] = new LootItemInstance(lootItem, itemData.Stars);
            }
            else
            {
                Debug.LogWarning($"LootItem no encontrado: {itemData.ToLootItemInstance(lootItem).Data.itemName}");
            }
        }

        return equippedItems;
    }

    private void SaveStats()
    {
        string statsJson = JsonUtility.ToJson(_stats);
        PlayerPrefs.SetString(PLAYER_STATS_KEY, statsJson);
        PlayerPrefs.Save();
    }

    private PlayerGlobalStats LoadStats()
    {
        string json = PlayerPrefs.GetString(PLAYER_STATS_KEY, "{}");
        return JsonUtility.FromJson<PlayerGlobalStats>(json) ?? new PlayerGlobalStats();
    }

    private LootItem FindLootItemByName(string itemName)
    {
        return _dataBaseService.GetListItemLoot()
            .Find(item => item.Data.itemName == itemName);
    }

    public Dictionary<EquipmentSlot, LootItemInstance> GetEquippedItem()
    {
        return _equippedItems;
    }
}

[System.Serializable]
public class EquippedItemsData
{
    public List<LootItemInstanceData> Items = new();
}