using System.Collections;
using UnityEngine;

namespace Bellseboss.States
{
    public class CheckState : IBattleState
    {
        public CheckState()
        {
            NextStateId = EnemyStatesConfiguration.FinalState;
        }

        public IEnumerator DoEnter()
        {
            Debug.Log("CheckState: Entering state");
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