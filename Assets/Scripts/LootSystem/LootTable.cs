using System.Collections.Generic;
using UnityEngine;

namespace LootSystem
{
    [CreateAssetMenu(fileName = "NewLootTable", menuName = "Loot System/Loot Table")]
    public class LootTable : ScriptableObject
    {
        public int MaxDrops = 3; // Máximo de items que puede soltar el enemigo
        public List<LootEntry> LootEntries = new();
    }
}