using UnityEngine;

namespace Bellseboss
{
    public class WaitStartState : IDungeonState
    {
        private readonly DungeonRunner runner;
        private bool startRequested;

        public WaitStartState(DungeonRunner runner)
        {
            this.runner = runner;
        }

        public void Enter()
        {
            Debug.Log("Entrando al estado de espera.");
            // Mostrar UI con botón de comenzar dungeon
        }

        public void Exit()
        {
            Debug.Log("Saliendo del estado de espera.");
            // Ocultar UI de inicio si es necesario
        }

        public void RequestStart()
        {
            StartDungeon();
        }

        private void StartDungeon()
        {
            Debug.Log("Dungeon iniciado");

            var map = runner.GetDungeonMap();
            var startNode = map.GetNode(map.StartNodeId);

            runner.SetState(new DungeonNodeState(runner, startNode));
        }
    }
}