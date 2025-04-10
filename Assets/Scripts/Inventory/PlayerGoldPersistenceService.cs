using System;
using UnityEngine;

namespace Inventory
{
    public class PlayerGoldPersistenceService : IPlayerGoldPersistenceService
    {
        private IDataPersistenceService _dataPersistenceService;
        private const string GoldKey = "player_gold";
        private GoldData _goldData;

        public PlayerGoldPersistenceService(IDataPersistenceService dataPersistenceService)
        {
            _dataPersistenceService = dataPersistenceService;
            _goldData = _dataPersistenceService.Load(GoldKey, new GoldData());
        }

        public void SaveGold(int goldAmount)
        {
            _goldData = new GoldData { gold = goldAmount };
            _dataPersistenceService.Save(GoldKey, _goldData);
            Debug.Log($"Gold saved: {_goldData.gold}");
            OnGoldChanged?.Invoke(_goldData.gold);
        }

        // Load the amount of gold
        public int LoadGold()
        {
            return _dataPersistenceService.Load(GoldKey, new GoldData()).gold;
        }

        public void AddGold(int totalValue)
        {
            _goldData.gold += totalValue;
            SaveGold(_goldData.gold);
            Debug.Log($"Gold added: {totalValue}, new total: {_goldData.gold}");
        }

        public event Action<int> OnGoldChanged;
    }

    [Serializable]
    public class GoldData
    {
        public int gold;
    }
}