using Items.Runtime;

namespace Items
{
    public interface ILootSource
    {
        LootItemInstance[] GetLoot();
    }
}