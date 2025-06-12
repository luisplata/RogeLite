using UnityEngine;

namespace Bellseboss
{
    public class DungeonNodeState : IDungeonState
    {
        private readonly DungeonRunner runner;
        private readonly DungeonNode currentNode;

        public DungeonNodeState(DungeonRunner runner, DungeonNode node)
        {
            this.runner = runner;
            this.currentNode = node;
        }

        public void Enter()
        {
            Debug.Log($"Nodo actual: {currentNode.id} - Tipo: {currentNode.nodeType}");
            runner.SetScene(currentNode);
            if (!runner.PlayerWon && currentNode.nodeType == NodeType.Final)
            {
                FinishDungeon();
                return;
            }

            switch (currentNode.nodeType)
            {
                case NodeType.Final:
                    FinishDungeon();
                    // Puedes cambiar de estado a VictoryState, por ejemplo
                    break;

                case NodeType.Combat:
                    PrepareCombat();
                    break;

                case NodeType.Farm:
                    GoToNextNode();
                    break;

                case NodeType.Empty:
                default:
                    GoToNextNode();
                    break;
            }
        }

        private static void FinishDungeon()
        {
            Debug.Log("¡Mazmorra completada!");
        }

        private void GoToNextNode()
        {
            if (currentNode.connectedNodeIds == null || currentNode.connectedNodeIds.Count == 0)
            {
                Debug.Log("No hay más nodos conectados. Consideramos esto como una victoria.");
                runner.SetState(new VictoryState(runner));
                return;
            }

            var nextNodeId = currentNode.connectedNodeIds[0];
            var nextNode = runner.GetDungeonMap().GetNode(nextNodeId);

            if (nextNode == null)
            {
                Debug.LogError($"El nodo con ID {nextNodeId} no se encontró en el mapa.");
                return;
            }

            runner.SetState(new DungeonNodeState(runner, nextNode));
        }

        private void PrepareCombat()
        {
            // Spawn enemigos, mostrar UI, etc.
            runner.SpawnEnemies(currentNode);
            Debug.Log($"Preparando combate en el nodo {currentNode.id}");

            // Mostrar botón de "Iniciar combate" y esperar al jugador
            runner.ShowCombatPreparationUI(() =>
            {
                Debug.Log("Entrando en combate...");
                runner.SetState(
                    new CombatState(runner.GetCombatManager(), runner.OnCombatFinished, runner, currentNode));
            });
        }

        public void Exit()
        {
        }
    }
}