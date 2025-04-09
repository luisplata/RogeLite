using System.Collections.Generic;
using System.Text;
using Bellseboss;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<LootItemInstance> items = new();

    public void AddItem(LootItemInstance item)
    {
        items.Add(item);
        //Debug.Log($"Added {item.Name} ({item.Stars}★) to inventory.");
    }

    public void RemoveItem(LootItemInstance item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            //Debug.Log($"Removed {item.Name} ({item.Stars}★) from inventory.");
        }
        else
        {
            Debug.LogWarning($"Tried to remove {item.Data.itemName}, but it's not in the inventory.");
        }
    }

    public bool HasItem(LootItemInstance item)
    {
        return items.Contains(item);
    }

    public List<LootItemInstance> GetAllItems()
    {
        Debug.Log($"Count of inventory ingame {items.Count}");
        return new List<LootItemInstance>(items); // Retorna una copia para evitar modificaciones externas
    }

    [ContextMenu("PrintInventory")]
    public void PrintInventory()
    {
        if (items.Count == 0)
        {
            //Debug.Log("Inventory is empty.");
            return;
        }

        StringBuilder sb = new();
        sb.AppendLine("🛠 **Inventory:**");

        foreach (var item in items)
        {
            sb.AppendLine($"- {item.Data.itemName} ({item.stars}★)");
        }

        Debug.Log(sb.ToString());
    }

    public string GetFormattedInventory()
    {
        if (items.Count == 0)
        {
            return "<b>Inventory is empty.</b>";
        }

        StringBuilder sb = new();
        sb.AppendLine("<b>Inventory:</b>");

        foreach (var item in items)
        {
            sb.AppendLine($"- {item.Data.itemName} (<color=yellow>{item.stars} Start</color>)");
        }

        PrintInventory();

        return sb.ToString();
    }
}