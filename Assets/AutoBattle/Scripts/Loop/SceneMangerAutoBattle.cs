using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Bellseboss
{
    public class SceneManagerAutoBattle : MonoBehaviour
    {
        [SerializeField] private CombatManager combatManager;
        [SerializeField] private DungeonMap dungeonMap;
        [SerializeField] UnityEvent<bool> onFinishDungeon;
        private DungeonNode currentNode;

        private void Start()
        {
            Configure();
        }

        private void Configure()
        {
            Debug.Log("Esperando a que el jugador inicie el dungeon...");
            // Mostrar UI de inicio, botón llama a StartDungeon()
        }

        public void StartDungeon()
        {
            currentNode = dungeonMap.GetNode(dungeonMap.StartNodeId);
            Debug.Log($"Inicio del dungeon. Nodo actual: {currentNode.id}");
            HandleCurrentNode();
        }

        private void HandleCurrentNode()
        {
            if (currentNode.nodeType == NodeType.Final)
            {
                ShowResultScreen(true); // Victoria
                return;
            }

            if (currentNode.nodeType == NodeType.Combat)
            {
                Debug.Log("Combate iniciado...");
                //Reset enemies, and players
                ResetCombatState();
                StartCoroutine(HandleCombatCoroutine());
            }
            else
            {
                GoToNextNode();
            }
        }

        private IEnumerator HandleCombatCoroutine()
        {
            yield return StartCoroutine(combatManager.CombatLoop());

            bool playerWon = combatManager.PlayerWon;
            OnCombatFinished(playerWon);
        }

        public void OnCombatFinished(bool playerWon)
        {
            if (!playerWon)
            {
                ShowResultScreen(false); // Derrota
                return;
            }

            GoToNextNode();
        }

        private void GoToNextNode()
        {
            if (currentNode.connectedNodeIds.Count == 0)
            {
                ShowResultScreen(true); // Si no hay más nodos, victoria
                return;
            }

            currentNode = dungeonMap.GetNode(currentNode.connectedNodeIds[0]);
            Debug.Log($"Avanzando al nodo: {currentNode.id}");
            HandleCurrentNode();
        }

        private void ResetCombatState()
        {
            combatManager.ResetSlimes();
            // Aquí podrías reiniciar el estado de los slimes, etc.
            Debug.Log("Estado del combate reiniciado.");
        }

        private void ShowResultScreen(bool victory)
        {
            Debug.Log(victory ? "¡Victoria! Fin de la mazmorra." : "¡Derrota!");
            onFinishDungeon?.Invoke(victory);
        }

        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}