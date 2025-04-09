using Items.Config;
using Items.Data;

namespace Items.Runtime
{
    public class ItemFactory
    {
        public LootItemInstance Create(LootItem lootItem, int stars)
        {
            // Crea una nueva instancia del LootItem con estadísticas
            return new LootItemInstance(lootItem, stars);
        }

        public LootItemInstance CreateFromData(LootItemInstanceData data, LootItem lootItem)
        {
            // Crea una instancia de LootItem desde los datos persistentes
            return new LootItemInstance(data, lootItem);
        }
    }
}