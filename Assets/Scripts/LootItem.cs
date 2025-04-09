using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewLootItem", menuName = "Loot System/Loot Item")]
public class LootItem : ScriptableObject
{
    public LootItemData Data = new();
}
