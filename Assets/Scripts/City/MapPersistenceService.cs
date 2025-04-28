using System;
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
            var centralTerrainGuid = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var firstGrassGuid = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"); // Un nuevo GUID para el primer Grass
            var secondGrassGuid = Guid.NewGuid(); // Agregamos un nuevo GUID para el segundo Grass
            var thirdGrassGuid = Guid.NewGuid();  // Y para el tercero

            var defaultMap = new MapData
            {
                CentralTerrain = new CentralTerrainData
                {
                    Id = "CentralTerrain",
                    Position = "0,0,0",
                    Guid = centralTerrainGuid.ToString()
                },
                Terrains = new List<TerrainData>
                {
                    new()
                    {
                        Id = "Grass",
                        Guid = firstGrassGuid.ToString(),
                        ParentGuid = centralTerrainGuid.ToString(),
                        Anchor = "E",
                        ConnectTo = "O",
                    },
                    new()
                    {
                        Id = "Grass",
                        Guid = secondGrassGuid.ToString(),
                        ParentGuid = centralTerrainGuid.ToString(),
                        Anchor = "N",
                        ConnectTo = "S",
                    },
                    new()
                    {
                        Id = "Grass",
                        Guid = thirdGrassGuid.ToString(),
                        ParentGuid = firstGrassGuid.ToString(), // ← conectamos al primer Grass
                        Anchor = "E",
                        ConnectTo = "O",
                    }
                }
            };

            return defaultMap;
        }


    }
}