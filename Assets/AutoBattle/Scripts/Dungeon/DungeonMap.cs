using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bellseboss
{
    [CreateAssetMenu(fileName = "NewDungeon", menuName = "Dungeon/Dungeon Map")]
    public class DungeonMap : ScriptableObject
    {
        public List<DungeonNode> Nodes;
        public string StartNodeId;

        public DungeonNode GetNode(string id)
        {
            return Nodes.FirstOrDefault(n => n.id == id);
        }
    }
}