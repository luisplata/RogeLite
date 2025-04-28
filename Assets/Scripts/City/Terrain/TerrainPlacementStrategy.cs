using System.Collections.Generic;
using UnityEngine;

namespace City.Terrain
{
    public class TerrainPlacementStrategy : ITerrainPlacementStrategy
    {
        private const float AnchorOffset = 9f;

        // Diccionario con las combinaciones predefinidas de anclas y sus offsets
        private static readonly Dictionary<(string, string), Vector3> OffsetMap = new()
        {
            { ("E", "O"), new Vector3(AnchorOffset, 0, 0) },
            { ("O", "E"), new Vector3(-AnchorOffset, 0, 0) },
            { ("N", "S"), new Vector3(0, 0, AnchorOffset) },
            { ("S", "N"), new Vector3(0, 0, -AnchorOffset) },
            { ("N", "N"), new Vector3(0, 0, AnchorOffset) },
            { ("S", "S"), new Vector3(0, 0, -AnchorOffset) },
            { ("E", "E"), new Vector3(AnchorOffset, 0, 0) },
            { ("O", "O"), new Vector3(-AnchorOffset, 0, 0) },
            { ("N", "E"), new Vector3(AnchorOffset, 0, AnchorOffset) },
            { ("N", "O"), new Vector3(-AnchorOffset, 0, AnchorOffset) },
            { ("S", "E"), new Vector3(AnchorOffset, 0, -AnchorOffset) },
            { ("S", "O"), new Vector3(-AnchorOffset, 0, -AnchorOffset) },
        };

        // Método para obtener el desplazamiento en base a las anclas
        private static Vector3 GetAnchorOffset(string lastAnchor, string currentAnchor)
        {
            // Primero comprobamos la combinación directa
            if (OffsetMap.TryGetValue((lastAnchor, currentAnchor), out var offset))
            {
                return offset;
            }

            // Si no se encuentra, intentamos la combinación invertida
            if (OffsetMap.TryGetValue((currentAnchor, lastAnchor), out offset))
            {
                return offset;
            }

            // Si no existe la combinación, se devuelve Vector3.zero y advertimos en consola
            Debug.LogWarning($"Anchor pair not configured: ({currentAnchor}, {lastAnchor})");
            return Vector3.zero;
        }

        // Calcula la posición del terreno en función de las anclas
        public Vector3 CalculatePosition(Transform lastAnchor, Transform currentAnchor)
        {
            return GetAnchorOffset(lastAnchor.name, currentAnchor.name);
        }

        public Vector3 CalculatePositionNode(string lastAnchor, string currentAnchor)
        {
            throw new System.NotImplementedException();
        }
    }
}
