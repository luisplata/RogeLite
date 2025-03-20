using System.Collections.Generic;
using System.Text;
using Bellseboss;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> items = new();

    public void AddItem(Item item)
    {
        items.Add(item);
        //Debug.Log($"Added {item.Name} ({item.Stars}★) to inventory.");
    }

    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            //Debug.Log($"Removed {item.Name} ({item.Stars}★) from inventory.");
        }
        else
        {
            Debug.LogWarning($"Tried to remove {item.Name}, but it's not in the inventory.");
        }
    }

    public bool HasItem(Item item)
    {
        return items.Contains(item);
    }

    public List<Item> GetAllItems()
    {
        Debug.Log($"Count of inventory ingame {items.Count}");
        return new List<Item>(items); // Retorna una copia para evitar modificaciones externas
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
            sb.AppendLine($"- {item.Name} ({item.Stars}★)");
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
            sb.AppendLine($"- {item.Name} (<color=yellow>{item.Stars} Start</color>)");
        }

        PrintInventory();

        return sb.ToString();
    }
}