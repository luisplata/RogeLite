using UnityEngine;

namespace Bellseboss
{
    public class VictoryState : IDungeonState
    {
        private readonly DungeonRunner runner;

        public VictoryState(DungeonRunner runner)
        {
            this.runner = runner;
        }

        public void Enter()
        {
            Debug.Log("¡Felicidades! Has completado la mazmorra.");
            // Aquí puedes mostrar la UI de victoria, invocar eventos, etc.
        }

        public void Exit() { }

        public void Update() { }
    }
}