using System.Collections.Generic;
using UnityEngine;

namespace City.Terrain
{
    public class TerrainNode
    {
        public string Id { get; set; }
        public string Guid { get; set; }
        public Vector3 Position { get; set; }
        public Dictionary<string, TerrainNode> Neighbors { get; set; } = new Dictionary<string, TerrainNode>();
        public Dictionary<string, bool> Anchors { get; set; } = new Dictionary<string, bool>();

        public BaseTerrain TerrainInstance { get; set; } // Campo para almacenar la instancia del terreno

        // Constructor que asigna valores por defecto a los anchors (N, S, E, O)
        public TerrainNode(string id, string guid, Vector3 position)
        {
            Id = id;
            Guid = guid;
            Position = position;
            Anchors.Add("N", false);
            Anchors.Add("S", false);
            Anchors.Add("E", false);
            Anchors.Add("O", false);
        }

        // Añadir un vecino al nodo
        public void AddNeighbor(string direction, TerrainNode neighbor)
        {
            if (!Anchors.ContainsKey(direction))
            {
                Debug.LogError($"Direction {direction} is not a valid anchor.");
                return;
            }

            if (IsAnchorOccupied(direction))
            {
                Debug.LogWarning($"Anchor {direction} is already occupied.");
                return;
            }

            Neighbors[direction] = neighbor;
            Anchors[direction] = true; // Marcar el anchor como ocupado
        }

        // Verificar si un anchor está ocupado
        public bool IsAnchorOccupied(string direction)
        {
            return Anchors.ContainsKey(direction) && Anchors[direction];
        }

        // Marcar un anchor como ocupado
        public void MarkAnchorAsOccupied(string direction)
        {
            if (Anchors.ContainsKey(direction))
            {
                Anchors[direction] = true;
            }
        }

        // Marcar un anchor como libre
        public void MarkAnchorAsFree(string direction)
        {
            if (Anchors.ContainsKey(direction))
            {
                Anchors[direction] = false;
            }
        }

        // Obtener el anchor que está conectado a un vecino
        public string GetAnchorFacing(TerrainNode neighbor)
        {
            foreach (var kvp in Neighbors)
            {
                if (kvp.Value == neighbor)
                    return kvp.Key;
            }

            return null;
        }

        // Eliminar un vecino de un anchor
        public void RemoveNeighbor(string direction)
        {
            if (Neighbors.ContainsKey(direction))
            {
                Neighbors.Remove(direction);
                Anchors[direction] = false; // Marcar el anchor como libre
            }
            else
            {
                Debug.LogWarning($"No neighbor found in the {direction} direction.");
            }
        }

        // Obtener el vecino conectado a un anchor
        public TerrainNode GetNeighborAt(string direction)
        {
            if (Neighbors.ContainsKey(direction))
                return Neighbors[direction];
            return null;
        }
    }
}
