using System.Collections;
using UnityEngine;

namespace Bellseboss.States
{
    public class FinishState : IBattleState
    {
        public FinishState()
        {
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
            yield return null;
        }

        public int NextStateId { get; }
    }
}