using System.Collections;
using UnityEngine;

namespace Bellseboss.States
{
    public class FinishState : IBattleState
    {
        private readonly ICombatManagerResult _combatManager;

        public FinishState(ICombatManagerResult combatManager)
        {
            _combatManager = combatManager;
            NextStateId = EnemyStatesConfiguration.BreakingState;
        }

        public IEnumerator DoEnter()
        {
            Debug.Log("FinishState: Entering state");
            yield return null;
        }

        public IEnumerator DoAction()
        {
            yield return null;
        }

        public IEnumerator DoExit()
        {
            Debug.Log($"Result: {_combatManager.GetResult()}");
            yield return null;
        }

        public int NextStateId { get; }
    }
}