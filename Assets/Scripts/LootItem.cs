using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLootItem", menuName = "Loot System/Loot Item")]
public class LootItem : ScriptableObject
{
    public string itemName;
    public LootType lootType;
    public EquipmentSlot equipmentSlot; // Nuevo: Define en qué slot se puede equipar
    public Dictionary<string, float> baseStats = new(); // Nuevo: Estadísticas base

    public void InitializeStats()
    {
        // Si el objeto es equipable, aseguramos que tenga estadísticas iniciales
        if (lootType == LootType.Equipable)
        {
            if (!baseStats.ContainsKey("Attack")) baseStats["Attack"] = 0;
            if (!baseStats.ContainsKey("Defense")) baseStats["Defense"] = 0;
            if (!baseStats.ContainsKey("Speed")) baseStats["Speed"] = 0;
            if (!baseStats.ContainsKey("CooldownReduction")) baseStats["CooldownReduction"] = 0;
        }
    }
}