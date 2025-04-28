using System.Collections;
using System.Collections.Generic;
using City.Data;
using UnityEngine;
using TerrainData = City.Data.TerrainData;

namespace City.Terrain
{
    public class TerrainManager
    {
        private Dictionary<string, TerrainNode> terrainNodes = new Dictionary<string, TerrainNode>();
        private ITerrainPlacementStrategy placementStrategy;
        private ITerrainFactory _terrainFactory;

        public TerrainManager(List<BaseTerrain> terrainPrefabs)
        {
            placementStrategy = new TerrainPlacementStrategyNode();
            _terrainFactory = new TerrainFactory(terrainPrefabs);
        }

        public TerrainNode CreateTerrain(string id, string guid, Vector3 position)
        {
            var newNode = new TerrainNode(id, guid, position);
            terrainNodes[guid] = newNode;
            // Crear el terreno físico (GameObject)
            var terrainInstance = _terrainFactory.CreateTerrain(id); // Aquí usas tu sistema de fábricas
            terrainInstance.SetGuid(System.Guid.Parse(guid));
            terrainInstance.transform.position = position;

            // Relacionarlo con el nodo
            newNode.TerrainInstance = terrainInstance;
            return newNode;
        }

        public void ConnectTerrains(TerrainNode parentNode, TerrainNode newNode, string parentAnchor,
            string newNodeAnchor)
        {
            if (parentNode.IsAnchorOccupied(parentAnchor))
            {
                Debug.LogError($"Anchor {parentAnchor} on parent node {parentNode.Id} is already occupied.");
                return;
            }

            parentNode.AddNeighbor(parentAnchor, newNode);
            newNode.AddNeighbor(newNodeAnchor, parentNode);

            parentNode.MarkAnchorAsOccupied(parentAnchor);
            newNode.MarkAnchorAsOccupied(newNodeAnchor);

            // 🔥 Aquí usamos los nombres directamente
            var offset = placementStrategy.CalculatePositionNode(parentAnchor, newNodeAnchor);

            newNode.Position = parentNode.Position + offset;
            newNode.TerrainInstance.transform.position = newNode.Position;

            // Conectar físicamente los anchors (si quieres seguir haciéndolo)
            var parentAnchorObj = parentNode.TerrainInstance.GetAnchor(parentAnchor);
            var newNodeAnchorObj = newNode.TerrainInstance.GetAnchor(newNodeAnchor);

            parentAnchorObj.ConnectTo(newNodeAnchorObj);
            newNodeAnchorObj.ConnectTo(parentAnchorObj);
        }


        public void Initialize(MapData mapData)
        {
            // Primero, creamos el terreno central, que sí tiene posición
            var centralTerrain = CreateTerrain(
                mapData.CentralTerrain.Id,
                mapData.CentralTerrain.Guid,
                StringToVector3(mapData.CentralTerrain.Position)
            );

            // Ahora creamos el resto de los terrenos
            foreach (var terrainData in mapData.Terrains)
            {
                if (!terrainNodes.TryGetValue(terrainData.ParentGuid, out var parentNode))
                {
                    Debug.LogError($"Parent terrain with GUID {terrainData.ParentGuid} not found.");
                    continue;
                }

                // Los hijos no tienen posición, así que inicializamos en Vector3.zero
                var newTerrainNode = CreateTerrain(
                    terrainData.Id,
                    terrainData.Guid,
                    Vector3.zero // ¡Importante! No tomamos posición desde JSON
                );

                // Luego los conectamos
                ConnectTerrains(parentNode, newTerrainNode, terrainData.ConnectTo, terrainData.Anchor);
            }

            PrintTerrainSummary();
        }

        private Vector3 StringToVector3(string position)
        {
            var values = position.Split(',');
            return new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
        }

        public MapData ExportMapData()
        {
            MapData mapData = new MapData
            {
                CentralTerrain = new CentralTerrainData(),
                Terrains = new List<TerrainData>()
            };
            foreach (var node in terrainNodes.Values)
            {
                if (node.Id == "CentralTerrain")
                {
                    mapData.CentralTerrain = new CentralTerrainData
                    {
                        Id = node.Id,
                        Guid = node.Guid,
                        Position = $"{node.Position.x},{node.Position.y},{node.Position.z}"
                    };
                }
                else
                {
                    foreach (var neighbor in node.Neighbors)
                    {
                        var parentNode = neighbor.Value;
                        var terrainData = new TerrainData
                        {
                            Id = node.Id,
                            Guid = node.Guid,
                            ParentGuid = parentNode.Guid,
                            Anchor = neighbor.Key, // el anchor de ESTE nodo que se conecta
                            ConnectTo = parentNode
                                .GetAnchorFacing(node) // función que debes agregar para saber el anchor opuesto
                        };

                        mapData.Terrains.Add(terrainData);
                        break; // Solo hacemos 1 conexión hacia su "parent" (el primero que encontramos)
                    }
                }
            }

            return mapData;
        }

        public void PrintTerrainSummary()
        {
            var mapData = ExportMapData();
            string json = JsonUtility.ToJson(mapData, true); // "true" = pretty print (bien indentado)

            Debug.Log("===== Exported MapData JSON =====");
            Debug.Log(json);
            Debug.Log("=================================");
        }

        // Agregar un nuevo nodo al mapa actual
        public TerrainNode AddTerrainNode(string id, string guid, string parentGuid, string parentAnchor,
            string newNodeAnchor)
        {
            if (!terrainNodes.TryGetValue(parentGuid, out var parentNode))
            {
                Debug.LogError($"Parent node with GUID {parentGuid} not found.");
                return null;
            }

            if (parentNode.IsAnchorOccupied(parentAnchor))
            {
                Debug.LogError($"Anchor {parentAnchor} on parent {parentNode.Id} already occupied.");
                return null;
            }

            var newNode = CreateTerrain(id, guid, Vector3.zero);

            newNode.Position = parentNode.Position +
                               placementStrategy.CalculatePositionNode(parentAnchor, newNodeAnchor);

            AddConnection(parentNode, newNode, parentAnchor, newNodeAnchor);

            return newNode;
        }

        // Remover un nodo (y desconectarlo de sus vecinos)
        public void RemoveTerrainNode(string guid)
        {
            if (!terrainNodes.TryGetValue(guid, out var node))
            {
                Debug.LogError($"Node with GUID {guid} not found.");
                return;
            }

            // Desconectar de todos los vecinos
            foreach (var kvp in node.Neighbors)
            {
                var neighbor = kvp.Value;
                var anchorFromNeighbor = neighbor.GetAnchorFacing(node);

                if (anchorFromNeighbor != null)
                {
                    neighbor.RemoveNeighbor(anchorFromNeighbor);
                }
            }

            terrainNodes.Remove(guid);
            Debug.Log($"Node {node.Id} removed.");
        }

        // Mover un nodo a una nueva posición manualmente
        public void MoveTerrainNode(string guid, Vector3 newPosition)
        {
            if (terrainNodes.TryGetValue(guid, out var node))
            {
                node.Position = newPosition;
                Debug.Log($"Node {node.Id} moved to {newPosition}.");
            }
            else
            {
                Debug.LogError($"Node with GUID {guid} not found.");
            }
        }

        // Agrega conexión entre dos nodos
        private void AddConnection(TerrainNode parent, TerrainNode child, string parentAnchor, string childAnchor)
        {
            parent.AddNeighbor(parentAnchor, child);
            child.AddNeighbor(childAnchor, parent);

            parent.MarkAnchorAsOccupied(parentAnchor);
            child.MarkAnchorAsOccupied(childAnchor);
        }

        public Dictionary<string, TerrainNode> GetTerrainNodes()
        {
            return terrainNodes;
        }
    }
}