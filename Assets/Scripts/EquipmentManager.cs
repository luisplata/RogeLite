using System.Collections.Generic;
using Bellseboss;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    private Dictionary<EquipmentSlot, LootItemInstance> equippedItems = new();
    private PlayerStats playerStats;

    public void Initialize(PlayerStats stats)
    {
        playerStats = stats;
    }

    public bool EquipItem(LootItemInstance item)
    {
        if (!item.IsEquipable()) return false;

        if (item.Slot == EquipmentSlot.TwoHandedWeapon)
        {
            UnequipItem(EquipmentSlot.LeftHandWeapon);
            UnequipItem(EquipmentSlot.RightHandWeapon);
        }
        else if (item.Slot == EquipmentSlot.LeftHandWeapon || item.Slot == EquipmentSlot.RightHandWeapon)
        {
            UnequipItem(EquipmentSlot.TwoHandedWeapon);
        }

        equippedItems[item.Slot] = item;
        ApplyItemStats(item);
        return true;
    }

    public void UnequipItem(EquipmentSlot slot)
    {
        if (equippedItems.TryGetValue(slot, out LootItemInstance item))
        {
            RemoveItemStats(item);
            equippedItems.Remove(slot);
        }
    }

    private void ApplyItemStats(LootItemInstance item)
    {
        foreach (var stat in item.stats)
        {
            playerStats.ApplyStat(stat);
        }
    }

    private void RemoveItemStats(LootItemInstance item)
    {
        foreach (var stat in item.stats)
        {
            playerStats.ApplyStat(stat);
        }
    }

    public string GetFormattedEquipment()
    {
        if (equippedItems.Count == 0) return "<b>No equipment equipped.</b>";

        System.Text.StringBuilder sb = new();
        sb.AppendLine("<b>⚔ Equipped Items:</b>");

        foreach (var entry in equippedItems)
        {
            sb.AppendLine($"- <b>{entry.Key}:</b> {entry.Value.itemName} (<color=yellow>{entry.Value.stars}★</color>)");
        }

        return sb.ToString();
    }
}