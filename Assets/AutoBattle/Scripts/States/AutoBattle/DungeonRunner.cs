using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Bellseboss
{
    public class DungeonRunner : MonoBehaviour
    {
        [SerializeField] private CombatManager combatManager;
        [SerializeField] private DungeonMap dungeonMap;
        [SerializeField] private UnityEvent onDungeonFinished;

        private DungeonStateMachine stateMachine;

        private WaitStartState waitStartState;
        public bool PlayerWon => combatManager.PlayerWon;

        private void Start()
        {
            combatManager.Configure();
            waitStartState = new WaitStartState(this);
            stateMachine = new DungeonStateMachine();
            stateMachine.SetState(waitStartState);
        }

        public void ExitToWaitState()
        {
            waitStartState.RequestStart();
        }

        public void SetState(IDungeonState newState)
        {
            stateMachine.SetState(newState);
        }

        public DungeonMap GetDungeonMap()
        {
            return dungeonMap;
        }

        public CombatManager GetCombatManager()
        {
            return combatManager;
        }

        public void OnCombatFinished(bool won)
        {
            // Cambiar de estado después del combate si lo deseas.
            Debug.Log($"Combate finalizado. ¿Ganó el jugador? {won}");
        }


        public void SpawnEnemies(DungeonNode node)
        {
            // Lógica para spawnear enemigos según los datos del nodo
            combatManager.ResetSlimes();
        }

        public void ShowCombatPreparationUI(UnityAction onConfirm)
        {
            // Muestra botones y espera a que el jugador presione "Iniciar"
            // Cuando lo hace, llama onConfirm.Invoke()
            StartCoroutine(ShowUiToStartCombat(onConfirm));
        }

        private IEnumerator ShowUiToStartCombat(UnityAction onConfirm)
        {
            Debug.Log("Mostrando UI de preparación de combate...");
            yield return new WaitForSeconds(5f); // Simula el tiempo que tarda en mostrar la UI

            Debug.Log("UI de preparación de combate mostrada y botón presionado.");
            // Aquí podrías mostrar un panel con un botón "Iniciar combate"
            // Por ahora, simplemente invocamos el callback directamente
            onConfirm.Invoke();
        }


        public void GoToNextNode()
        {
            onDungeonFinished?.Invoke();
        }

        public void ResetDungeon()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}