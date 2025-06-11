using System.Collections;
using UnityEngine;

namespace Bellseboss.States
{
    public class BeggingState : IBattleState
    {
        public BeggingState()
        {
            NextStateId = EnemyStatesConfiguration.TurnState;
        }

        public IEnumerator DoEnter()
        {
            Debug.Log("BeggingState: Entering state");
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