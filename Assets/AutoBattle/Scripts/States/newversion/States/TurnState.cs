using System.Collections;
using UnityEngine;

namespace Bellseboss.States
{
    public class TurnState : IBattleState
    {
        public TurnState()
        {
            NextStateId = EnemyStatesConfiguration.CheckState;
        }

        public IEnumerator DoEnter()
        {
            Debug.Log("TurnState: Entering state");
            yield return null;
        }

        public IEnumerator DoAction()
        {
            //wait time
            yield return new WaitForSeconds(3f);
        }

        public IEnumerator DoExit()
        {
            yield return null;
        }

        public int NextStateId { get; }
    }
}