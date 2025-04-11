using System.Collections.Generic;
using City.Data;

namespace City
{
    public class MapPersistenceService : IMapPersistenceService
    {
        private const string MAP_KEY = "MapData";
        private readonly IDataPersistenceService _dataPersistenceService;

        public MapPersistenceService(IDataPersistenceService dataPersistenceService)
        {
            _dataPersistenceService = dataPersistenceService;
        }

        public void SaveMap(MapData mapData)
        {
            _dataPersistenceService.Save(MAP_KEY, mapData);
        }

        public MapData LoadMap()
        {
            var mapData = _dataPersistenceService.Load(MAP_KEY, new MapData());
            
            if (string.IsNullOrEmpty(mapData.CentralTerrain.Id))
            {
                mapData = GenerateDefaultMap();
                SaveMap(mapData);
            }

            return mapData;
        }

        private MapData GenerateDefaultMap()
        {
            var defaultMap = new MapData
            {
                CentralTerrain = new CentralTerrainData
                {
                    Id = "CentralTerrain",
                    Position = "0,0,0"
                },
                Terrains = new List<TerrainData>
                {
                    new() { Id = "Grass", Anchor = "E", ConnectTo = "O" },
                    new() { Id = "Grass", Anchor = "N", ConnectTo = "S" },
                    new() { Id = "Grass", Anchor = "E", ConnectTo = "O" },
                }
            };

            return defaultMap;
        }
    }
}