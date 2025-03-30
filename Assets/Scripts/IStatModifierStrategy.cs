using System.Collections.Generic;

namespace Bellseboss
{
    public interface IStatModifierStrategy
    {
        List<BaseStatsOnItem> ModifyStats(List<BaseStatsOnItem> baseStats, int stars);
    }
}