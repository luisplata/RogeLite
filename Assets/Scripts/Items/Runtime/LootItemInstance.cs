using System;
using System.Collections.Generic;
using Items.Config;
using Items.Data;
using UnityEngine;

namespace Items.Runtime
{
    [Serializable]
    public class LootItemInstance
    {
        public string UUID { get; private set; }
        public LootItemData Data { get; private set; }
        public int Stars { get; set; }
        public List<BaseStatsOnItem> BaseStats { get; set; } = new();

        // Constructor cuando se crea un item desde un LootItem (configuración)
        public LootItemInstance(LootItem lootItem, int stars)
        {
            Data = lootItem.Data;
            Stars = stars;
            UUID = Guid.NewGuid().ToString();  // Generamos un UUID único para cada instancia

            // Añadimos estadísticas base (puedes calcularlas de manera dinámica según las estrellas o la ranura)
            AddBaseStats();
        }

        // Constructor para cuando cargamos la instancia desde la persistencia
        public LootItemInstance(LootItemInstanceData data, LootItem lootItem)
        {
            Data = lootItem.Data;  // Lo cargamos usando el LootItem correspondiente
            Stars = data.Stars;
            BaseStats = data.BaseStats;
            UUID = lootItem.Data.UUID;  // Usamos el UUID del LootItem configurado
        }

        private void AddBaseStats()
        {
            // Aquí es donde puedes añadir lógica para calcular las estadísticas en base a las estrellas, equipo, etc.
            if (Data.equipmentSlot != EquipmentSlot.NONE)
            {
                // Esto es solo un ejemplo, puedes modificarlo como quieras según la lógica del juego
                BaseStats.Add(new BaseStatsOnItem(StatType.Attack, Stars * 10));
                BaseStats.Add(new BaseStatsOnItem(StatType.Defense, Stars * 5));
            }
            else
            {
                BaseStats.Add(new BaseStatsOnItem(StatType.Heal, Stars * 10));
            }
        }

        public bool IsEquipable() => Data.equipmentSlot != EquipmentSlot.NONE;
    }
}