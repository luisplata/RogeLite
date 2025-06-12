using UnityEngine;

namespace Bellseboss
{
    public class FarmState : IDungeonState
    {
        private readonly DungeonRunner runner;
        private readonly DungeonNode currentNode;

        public FarmState(DungeonRunner runner, DungeonNode currentNode)
        {
            this.runner = runner;
            this.currentNode = currentNode;
        }

        public void Enter()
        {
            Debug.Log($"Entrando al estado de granja en el nodo {currentNode.id}");
            // Aquí puedes implementar la lógica específica de la granja
            // Por ejemplo, recolectar recursos, mejorar habilidades, etc.
            runner.SetState(new DungeonNodeState(runner,
                runner.GetDungeonMap().GetNode(currentNode.connectedNodeIds[0])));
        }


        public void Exit()
        {
        }
    }
}