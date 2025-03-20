using System.Collections.Generic;

namespace Bellseboss
{
    public class Item
    {
        public string Name { get; }
        public LootType Type { get; }
        public int Stars { get; }
        public Dictionary<string, float> Stats { get; set; } = new();
        public EquipmentSlot? Slot { get; } // Nuevo campo opcional

        public Item(string name, LootType type, int stars, EquipmentSlot? slot = null)
        {
            Name = name;
            Type = type;
            Stars = stars;
            Slot = slot;
        }

        public bool IsEquipable() => Slot != null;
    }
}