using UnityEngine;
using System.Collections.Generic;
using Bellseboss;

public class PlayerConfigurationService : IPlayerConfigurationService
{
    private const string CHARACTER_TYPE_KEY = "CharacterType";
    private const string EQUIPPED_ITEMS_KEY = "EquippedItems";
    private const string PLAYER_STATS_KEY = "PlayerStats";

    private CharacterType _characterType;
    private Dictionary<EquipmentSlot, LootItemInstance> _equippedItems = new();
    private PlayerGlobalStats _stats = new();

    public PlayerConfigurationService()
    {
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
        _equippedItems[item.Slot] = item;
        SaveEquippedItems();
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

        string json = JsonUtility.ToJson(equippedData);
        Debug.Log($"{EQUIPPED_ITEMS_KEY} Saving equipped items: {json}");
        PlayerPrefs.SetString(EQUIPPED_ITEMS_KEY, json);
        PlayerPrefs.Save();
    }

    private Dictionary<EquipmentSlot, LootItemInstance> LoadEquippedItems()
    {
        string json = PlayerPrefs.GetString(EQUIPPED_ITEMS_KEY, "{}");
        Debug.Log($"{EQUIPPED_ITEMS_KEY} Loaded equipped items: {json}");

        EquippedItemsData equippedData = JsonUtility.FromJson<EquippedItemsData>(json) ?? new EquippedItemsData();
        Dictionary<EquipmentSlot, LootItemInstance> equippedItems = new();

        foreach (var itemData in equippedData.Items)
        {
            LootItem lootItem = FindLootItemByName(itemData.itemName);
            if (lootItem != null)
            {
                equippedItems[itemData.slot] = new LootItemInstance(itemData, lootItem);
            }
            else
            {
                Debug.LogWarning($"LootItem no encontrado: {itemData.itemName}");
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
        return ServiceLocator.Instance.GetService<IDataBaseService>().GetListItemLoot().Find(item => item.itemName == itemName);
    }

    public Dictionary<EquipmentSlot,LootItemInstance> GetEquippedItem()
    {
        return _equippedItems;
    }
}

[System.Serializable]
public class EquippedItemsData
{
    public List<LootItemInstanceData> Items = new();
}
