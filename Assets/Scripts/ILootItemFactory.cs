namespace Bellseboss
{
    public interface ILootItemFactory
    {
        LootItemInstance CreateLootItem(LootItem baseItem);
    }
}