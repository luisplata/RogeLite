using System;

namespace Items
{
    [Serializable]
    public class BaseStatsOnItem
    {
        public StatType statType; // En lugar de un ScriptableObject, usamos un enum
        public float statValue;

        public BaseStatsOnItem(StatType statType, float statValue)
        {
            this.statType = statType;
            this.statValue = statValue;
        }
    }
}