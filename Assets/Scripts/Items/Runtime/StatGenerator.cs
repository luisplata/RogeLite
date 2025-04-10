using System.Collections.Generic;

namespace Items.Runtime
{
    public class StatGenerator
    {
        private static readonly Dictionary<int, float> StatScalingFactor = new Dictionary<int, float>
        {
            { 1, 1.0f }, // 1 star = 100% stats
            { 2, 1.2f }, // 2 stars = 120% stats
            { 3, 1.5f }, // 3 stars = 150% stats
            { 4, 2.0f }, // 4 stars = 200% stats
            { 5, 2.5f } // 5 stars = 250% stats
        };

        public static List<BaseStatOnItem> GenerateStats(LootItem item, int stars)
        {
            List<BaseStatOnItem> generatedStats = new List<BaseStatOnItem>();

            float scalingFactor = StatScalingFactor.ContainsKey(stars) ? StatScalingFactor[stars] : 1.0f;

            foreach (var baseStat in item.BaseStats)
            {
                generatedStats.Add(new BaseStatOnItem
                {
                    StatType = baseStat.StatType,
                    Value = (int)(baseStat.Value * scalingFactor)
                });
            }

            return generatedStats;
        }
    }
}