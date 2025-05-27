using UnityEngine;

namespace Bellseboss
{
    public class DungeonExplorer : MonoBehaviour
    {
        public DungeonMap currentDungeon;
        private DungeonNode currentNode;

        void Start()
        {
            EnterDungeon(currentDungeon);
        }

        public void EnterDungeon(DungeonMap dungeon)
        {
            currentDungeon = dungeon;
            currentNode = currentDungeon.GetNode(dungeon.StartNodeId);
            Debug.Log($"Entraste a la mazmorra. Nodo inicial: {currentNode.id}");
            HandleCurrentNode();
        }

        public void MoveTo(string nodeId)
        {
            if (!currentNode.connectedNodeIds.Contains(nodeId))
            {
                Debug.Log("Movimiento inválido.");
                return;
            }

            currentNode = currentDungeon.GetNode(nodeId);
            Debug.Log($"Te moviste al nodo {currentNode.id}");
            HandleCurrentNode();
        }

        private void HandleCurrentNode()
        {
            switch (currentNode.nodeType)
            {
                case NodeType.Empty:
                    Debug.Log("Este nodo está vacío.");
                    break;

                case NodeType.Combat:
                    Debug.Log("¡Combate iniciado!");
                    // Aquí invocas tu sistema de combate
                    break;

                case NodeType.Final:
                    Debug.Log("¡Has llegado al final de la mazmorra!");
                    break;
            }
        }

        public void PrintAvailablePaths()
        {
            Debug.Log("Caminos disponibles:");
            foreach (var id in currentNode.connectedNodeIds)
            {
                Debug.Log($"- {id}");
            }
        }
    }
}