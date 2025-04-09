using Items.Data;
using UnityEngine;

namespace Items.Config
{
    [CreateAssetMenu(fileName = "NewLootItem", menuName = "Loot System/Loot Item")]
    public class LootItem : ScriptableObject
    {
        public LootItemData Data = new();
    }
}