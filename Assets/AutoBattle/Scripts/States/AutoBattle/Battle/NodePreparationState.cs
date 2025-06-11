using UnityEngine;

namespace Bellseboss
{
    public class NodePreparationState : IDungeonState
    {
        private readonly DungeonRunner runner;
        private readonly DungeonNode currentNode;

        public NodePreparationState(DungeonRunner runner, DungeonNode currentNode)
        {
            this.runner = runner;
            this.currentNode = currentNode;
        }

        public void Enter()
        {
            Debug.Log($"Preparando nodo {currentNode.id} de tipo {currentNode.nodeType}");

            if (currentNode.nodeType == NodeType.Combat)
            {
                PrepareCombat();
            }
            else
            {
                ContinueToNextNode();
            }
        }

        private void PrepareCombat()
        {
            // Spawn enemigos, mostrar UI, etc.
            runner.SpawnEnemies(currentNode);

            // Mostrar botón de "Iniciar combate" y esperar al jugador
            runner.ShowCombatPreparationUI(() =>
            {
                runner.SetState(new CombatState(runner.GetCombatManager(), runner.OnCombatFinished, runner,
                    currentNode));
            });
        }

        private void ContinueToNextNode()
        {
            runner.GoToNextNode(); // Esto debe cambiar el estado a otro NodePreparationState o FinalState
        }

        public void Exit()
        {
        }

        public void Update()
        {
        }
    }
}