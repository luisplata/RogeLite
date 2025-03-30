using System.Collections.Generic;

namespace Bellseboss
{
    public class StarBasedStatModifier : IStatModifierStrategy
    {
        public List<BaseStatsOnItem> ModifyStats(List<BaseStatsOnItem> baseStats, int stars)
        {
            List<BaseStatsOnItem> modifiedStats = new();
            foreach (var stat in baseStats)
            {
                modifiedStats.Add(new BaseStatsOnItem(stat.statType, stat.statValue * stars));
            }
            return modifiedStats;
        }
    }
}