using System.Collections;
using UnityEngine;

namespace Bellseboss.States
{
    public class CheckState : IBattleState
    {
        private readonly ICombatManagerCheck _combatManager;

        public CheckState(ICombatManagerCheck combatManager)
        {
            _combatManager = combatManager;
            NextStateId = EnemyStatesConfiguration.FinalState;
        }

        public IEnumerator DoEnter()
        {
            Debug.Log("CheckState: Entering state");
            yield return null;
        }

        public IEnumerator DoAction()
        {
            if (!_combatManager.IsBattleOver())
            {
                Debug.Log("CheckState: Battle is not over, proceeding to next state.");
                NextStateId = EnemyStatesConfiguration.TurnState;
            }
            else
            {
                Debug.Log("CheckState: Battle is over, proceeding to final state.");
                NextStateId = EnemyStatesConfiguration.FinalState;
            }

            yield return null;
        }

        public IEnumerator DoExit()
        {
            yield return null;
        }

        public int NextStateId { get; private set; }
    }
}