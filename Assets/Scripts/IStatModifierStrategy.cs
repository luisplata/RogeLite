using System.Collections.Generic;
using Items;

namespace Bellseboss
{
    public interface IStatModifierStrategy
    {
        List<BaseStatsOnItem> ModifyStats(List<BaseStatsOnItem> baseStats, int stars);
    }
}