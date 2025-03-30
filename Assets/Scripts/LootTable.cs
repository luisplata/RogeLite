using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bellseboss
{
    [CreateAssetMenu(fileName = "NewLootTable", menuName = "Loot System/Loot Table")]
    public class LootTable : ScriptableObject
    {
        [Serializable]
        public class LootEntry
        {
            public LootItem item;
            [Range(0f, 1f)] public float dropChance;
        }

        public List<LootEntry> lootEntries;
        public int maxDrops = 3; // Cantidad máxima de drops
    }
}
