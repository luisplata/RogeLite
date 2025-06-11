using UnityEngine;

namespace Bellseboss
{
    public class DefeatState : IDungeonState
    {
        private readonly DungeonRunner runner;

        public DefeatState(DungeonRunner runner)
        {
            this.runner = runner;
        }

        public void Enter()
        {
            Debug.Log("Has sido derrotado. Fin de la mazmorra.");
            // Mostrar UI de derrota, llamar eventos, etc.
        }

        public void Exit() { }

        public void Update() { }
    }
}