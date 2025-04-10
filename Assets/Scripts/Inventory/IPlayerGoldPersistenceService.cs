using System;

namespace Inventory
{
    public interface IPlayerGoldPersistenceService
    {
        void SaveGold(int goldAmount);
        int LoadGold();
        void AddGold(int totalValue);
        event Action<int> OnGoldChanged;
    }
}