using System;
using System.Collections.Generic;
using City.Data;
using City.Terrain;
using UnityEngine;
using TerrainData = City.Data.TerrainData;

namespace City
{
    public class RulesInCity : MonoBehaviour
    {
        [SerializeField] private List<BaseTerrain> _terrainPrefabs;

        private ITerrainFactory _terrainFactory;
        private ITerrainPlacementStrategy _placementStrategy;
        private List<BaseTerrain> _placedTerrains = new();
        private TerrainManager _terrainManager;
        private bool modeBuy;

        public void Configure(MapData terrainDataJson)
        {
            _terrainFactory = new TerrainFactory(_terrainPrefabs);
            _placementStrategy = new TerrainPlacementStrategy();

            Initialize(terrainDataJson);
        }

        private void Initialize(MapData terrainDataJson)
        {
            _terrainManager = new TerrainManager(_terrainPrefabs);
            _terrainManager.Initialize(terrainDataJson);
            foreach (var VARIABLE in _terrainManager.GetTerrainNodes())
            {
                _placedTerrains.Add(VARIABLE.Value.TerrainInstance);
            }
        }


        private static Vector3 StringToVector3(string position)
        {
            var values = position.Split(',');
            return new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
        }

        public void SetMode(bool modeBuy)
        {
            this.modeBuy = modeBuy;

            // Habilitar o deshabilitar el collider en los anchors de los terrenos colocados
            foreach (var terrain in _placedTerrains)
            {
                var anchors = terrain.GetComponentsInChildren<TerrainAnchor>();
                foreach (var anchor in anchors)
                {
                    if (anchor.ConnectedAnchor == null) // Solo habilitamos los anchors sin conexión
                    {
                        anchor.EnableCollider(modeBuy); // Habilitar o deshabilitar según el modo de compra
                    }
                }
            }

            // Si estamos en modo de compra, permitir añadir nuevos terrenos
            if (modeBuy)
            {
                // Aquí se podría agregar la lógica para agregar nuevos nodos o terrenos,
                // como la creación de un nuevo terreno cuando el usuario haga clic en algún lugar, por ejemplo.
            }
        }

        // Lógica para agregar un nuevo terreno al mapa
        public void AddNewTerrain(string id, string guid, string parentGuid, string parentAnchor, string newNodeAnchor)
        {
            var newNode = _terrainManager.AddTerrainNode(id, guid, parentGuid, parentAnchor, newNodeAnchor);

            if (newNode != null)
            {
                // Si la creación fue exitosa, agregar el nuevo terreno al listado
                _placedTerrains.Add(newNode.TerrainInstance);
            }
        }
    }
}