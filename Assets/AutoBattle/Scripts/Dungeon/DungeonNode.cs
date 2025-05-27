using System.Collections.Generic;

namespace Bellseboss
{
    public enum NodeType
    {
        Empty,
        Combat,
        Final
    }

    [System.Serializable]
    public class DungeonNode
    {
        public string id;
        public NodeType nodeType;
        public List<string> connectedNodeIds = new();
    }
}