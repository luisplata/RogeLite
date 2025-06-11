using System.Collections;
using UnityEngine;

namespace Bellseboss
{
    public class CombatState : IDungeonState
    {
        private CombatManager combatManager;
        private System.Action<bool> onCombatFinished;
        private readonly DungeonRunner runner;
        private readonly DungeonNode currentNode;

        public CombatState(CombatManager combatManager, System.Action<bool> onFinished, DungeonRunner runner,
            DungeonNode node)
        {
            this.combatManager = combatManager;
            onCombatFinished = onFinished;
            this.runner = runner;
            currentNode = node;
        }

        public void Enter()
        {
            combatManager.StartCoroutine(CombatRoutine());
        }

        private IEnumerator CombatRoutine()
        {
            yield return combatManager.CombatLoop();
            onCombatFinished?.Invoke(combatManager.PlayerWon);
            var nextNodeId = currentNode.connectedNodeIds[0];
            var nextNode = runner.GetDungeonMap().GetNode(nextNodeId);

            if (nextNode == null)
            {
                Debug.LogError($"El nodo con ID {nextNodeId} no se encontró en el mapa.");
            }
            else
            {
                runner.SetState(new DungeonNodeState(runner, nextNode));
            }
        }

        public void Exit()
        {
        }

        public void Update()
        {
        }
    }
}