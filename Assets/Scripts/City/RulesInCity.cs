using System.Collections.Generic;
using City.Data;
using City.Terrain;
using UnityEngine;

namespace City
{
    public class RulesInCity : MonoBehaviour
    {
        [SerializeField] private List<BaseTerrain> _terrainPrefabs;

        private ITerrainFactory _terrainFactory;
        private ITerrainPlacementStrategy _placementStrategy;
        private List<BaseTerrain> _placedTerrains = new();

        public void Configure(MapData terrainDataJson)
        {
            _terrainFactory = new TerrainFactory(_terrainPrefabs);
            _placementStrategy = new TerrainPlacementStrategy();

            Initialize(terrainDataJson);
        }

        private void Initialize(MapData terrainDataJson)
        {
            var terrainData = terrainDataJson;

            var centralTerrain = _terrainFactory.CreateTerrain(terrainData.CentralTerrain.Id);
            centralTerrain.transform.position = StringToVector3(terrainData.CentralTerrain.Position);
            _placedTerrains.Add(centralTerrain);

            var lastTerrain = centralTerrain;

            foreach (var info in terrainData.Terrains)
            {
                var newTerrain = _terrainFactory.CreateTerrain(info.Id);
                newTerrain.transform.position = lastTerrain.transform.position;
                var lastAnchor = lastTerrain.GetAnchor(info.ConnectTo);
                var currentAnchor = newTerrain.GetAnchor(info.Anchor);

                var newPosition = _placementStrategy.CalculatePosition(lastAnchor, currentAnchor);
                newTerrain.transform.position += newPosition;

                _placedTerrains.Add(newTerrain);
                lastTerrain = newTerrain;
            }
        }

        private static Vector3 StringToVector3(string position)
        {
            var values = position.Split(',');
            return new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
        }
    }
}