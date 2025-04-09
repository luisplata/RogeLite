using System;

namespace Items
{
    public class GoldService : IGoldService
    {
        private const string GoldSaveKey = "PLAYER_GOLD";

        private GoldData goldData;

        private readonly IDataPersistenceService saveLoadService;

        public GoldService(IDataPersistenceService saveLoadService)
        {
            this.saveLoadService = saveLoadService;
            LoadGold();
        }

        private void LoadGold()
        {
            goldData = saveLoadService.Load(GoldSaveKey, new GoldData());
        }

        private void SaveGold()
        {
            saveLoadService.Save(GoldSaveKey, goldData);
            OnGoldChanged?.Invoke(goldData.currentGold);
        }

        public int GetGold() => goldData.currentGold;

        public void AddGold(int amount)
        {
            goldData.currentGold += amount;
            SaveGold();
        }

        public bool SpendGold(int amount)
        {
            if (goldData.currentGold < amount)
                return false;

            goldData.currentGold -= amount;
            SaveGold();
            return true;
        }

        public event Action<int> OnGoldChanged;
    }
}

public interface IGoldService
{
    int GetGold();
    void AddGold(int amount);
    bool SpendGold(int amount);
    event Action<int> OnGoldChanged;
}
