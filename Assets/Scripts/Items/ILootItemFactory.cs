using Bellseboss.Items;
using Items.Config;
using Items.Runtime;

namespace Items
{
    public interface ILootItemFactory
    {
        LootItemInstance CreateLootItem(LootItem baseItem);
    }
}