using System.Collections.Generic;
using UnityEngine;

namespace Bellseboss
{
    public enum NodeType
    {
        Empty,
        Combat,
        Farm,
        Final
    }

    [System.Serializable]
    public class DungeonNode
    {
        public string id;
        public NodeType nodeType;
        public List<string> connectedNodeIds = new();
        public GameObject scenarioPrefab;
    }
}