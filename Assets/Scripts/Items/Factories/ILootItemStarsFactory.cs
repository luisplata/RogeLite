using Items.Runtime;

namespace Items.Factories
{
    public interface ILootItemStarsFactory
    {
        LootItemInstance CreateLootItem(LootItem baseItem);
    }
}