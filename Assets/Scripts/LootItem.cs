using UnityEngine;

[CreateAssetMenu(fileName = "NewLootItem", menuName = "Loot System/Loot Item")]
public class LootItem : ScriptableObject
{
    public string itemName;
    public LootType lootType;
}