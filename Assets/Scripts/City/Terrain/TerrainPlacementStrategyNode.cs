using System.Collections.Generic;
using UnityEngine;

namespace City.Terrain
{
    public class TerrainPlacementStrategyNode : ITerrainPlacementStrategy
    {
        private const float AnchorOffset = 9f;

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

        public Vector3 CalculatePosition(Transform lastAnchor, Transform currentAnchor)
        {
            throw new System.NotImplementedException();
        }

        public Vector3 CalculatePositionNode(string parentAnchorName, string newNodeAnchorName)
        {
            var key = (parentAnchorName, newNodeAnchorName);

            if (OffsetMap.TryGetValue(key, out var offset))
                return offset;

            Debug.LogWarning($"Anchor pair not configured: ({parentAnchorName}, {newNodeAnchorName})");
            return Vector3.zero;
        }
    }
}