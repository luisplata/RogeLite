using System.Collections.Generic;

namespace Bellseboss
{
    public class Item
    {
        public string Name { get; }
        public LootType Type { get; }
        public int Stars { get; }
        public Dictionary<string, float> Stats { get; set; } = new();

        public Item(string name, LootType type, int stars = 0)
        {
            Name = name;
            Type = type;
            Stars = stars;
        }
    }
}