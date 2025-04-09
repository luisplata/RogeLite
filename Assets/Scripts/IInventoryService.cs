using System.Collections.Generic;
using Bellseboss;

public interface IInventoryService
{
    void AddItem(LootItemInstance item);
    void RemoveItem(LootItemInstance item);
    void SellItem(LootItemInstance item, int price);
    void BuyItem(LootItemInstance item, int price);
    List<LootItemInstance> GetItems();
    void ClearInventory();
}

public class InventoryService : IInventoryService
{
    private readonly IDataPersistenceService _dataPersistenceService;
    private readonly List<LootItem> _lootItems;
    private const string InventoryKey = "Inventory";

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

    public void RemoveItem(LootItemInstance item)
    {
        InventoryData inventory = LoadInventory();
        inventory.Items.RemoveAll(x => x.itemName == item.itemName);
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
            LootItem lootItem = _lootItems.Find(item => item.itemName == itemData.itemName);
            if (lootItem != null)
                items.Add(new LootItemInstance(itemData, lootItem));
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

    private void SaveInventory(InventoryData inventory)
    {
        _dataPersistenceService.Save(InventoryKey, inventory);
    }
}
