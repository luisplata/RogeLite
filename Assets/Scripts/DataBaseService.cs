using System.Collections.Generic;
using UnityEngine;
using Bellseboss;

public class DataBaseService : MonoBehaviour, IDataBaseService
{
    [SerializeField] private List<LootItem> lootItems;
    private void Awake()
    {
        if (FindObjectsByType<DataBaseService>(FindObjectsSortMode.None).Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        ServiceLocator.Instance.RegisterService<IDataBaseService>(this);
        DontDestroyOnLoad(gameObject);

        LoadInventory();
    }

    public void AddItem(LootItemInstance item)
    {
        InventoryData inventory = LoadInventory();
        Debug.Log($"In Add item slot {item.Slot}");
        inventory.Items.Add(new LootItemInstanceData(item));

        string json = JsonUtility.ToJson(inventory);
        PlayerPrefs.SetString("Inventory", json);
        PlayerPrefs.Save();

        Debug.Log($"Json Saved: {json}");
    }

    public List<LootItemInstance> GetItems()
    {
        InventoryData inventory = LoadInventory();
        List<LootItemInstance> items = new();

        foreach (var itemData in inventory.Items)
        {
            LootItem lootItem = FindLootItemByName(itemData.itemName);
            if (lootItem != null)
            {
                items.Add(new LootItemInstance(itemData, lootItem));
            }
            else
            {
                Debug.LogWarning($"LootItem no encontrado: {itemData.itemName}");
            }
        }

        return items;
    }

    public List<LootItem> GetListItemLoot()
    {
        return lootItems;
    }

    private InventoryData LoadInventory()
    {
        string json = PlayerPrefs.GetString("Inventory", "{}");
        Debug.Log($"Json Loaded: {json}");

        InventoryData inventory = JsonUtility.FromJson<InventoryData>(json) ?? new InventoryData();
        return inventory;
    }

    public void ClearItems()
    {
        PlayerPrefs.DeleteKey("Inventory");
        PlayerPrefs.Save();
    }

    public void SaveInventory(Inventory inventory)
    {
        InventoryData inventoryData = LoadInventory();
        foreach (var item in inventory.GetAllItems())
        {
            Debug.Log($"In SaveInventory slot {item.Slot}");
            inventoryData.Items.Add(new LootItemInstanceData(item));
        }

        string json = JsonUtility.ToJson(inventoryData);
        PlayerPrefs.SetString("Inventory", json);
        PlayerPrefs.Save();
    }

    private LootItem FindLootItemByName(string itemName)
    {
        // Aquí deberías tener una lista de LootItems cargados en memoria
        return lootItems.Find(item => item.itemName == itemName);
    }
}
