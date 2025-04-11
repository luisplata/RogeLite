using System.Collections.Generic;
using UnityEngine;

namespace City.Terrain
{
    public class TerrainFactory : ITerrainFactory
    {
        private readonly List<BaseTerrain> _terrainPrefabs;

        public TerrainFactory(List<BaseTerrain> terrainPrefabs)
        {
            _terrainPrefabs = terrainPrefabs;
        }

        public BaseTerrain CreateTerrain(string id)
        {
            var prefab = _terrainPrefabs.Find(x => x.name == id);
            if (prefab == null)
            {
                Debug.LogError($"Terrain Prefab not found with id: {id}");
                return null;
            }

            return GameObject.Instantiate(prefab);
        }
    }
}