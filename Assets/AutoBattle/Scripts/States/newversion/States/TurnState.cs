using System.Collections;
using UnityEngine;

namespace Bellseboss.States
{
    public class TurnState : IBattleState
    {
        private readonly ICombatManagerTurn _combatManager;
        private SlimeMediator _slime;

        public TurnState(ICombatManagerTurn combatManager)
        {
            _combatManager = combatManager;
            NextStateId = EnemyStatesConfiguration.CheckState;
        }

        public IEnumerator DoEnter()
        {
            Debug.Log("TurnState: Entering state");
            //Determinar que slime es el que va a jugar
            _slime = _combatManager.GetNextSlime();
            yield return null;
        }

        public IEnumerator DoAction()
        {
            if (_slime.IsAlive)
            {
                yield return _combatManager.Coroutine(_slime.PerformAction(_slime, _combatManager.AllSlimes()));
            }
        }

        public IEnumerator DoExit()
        {
            yield return null;
        }

        public int NextStateId { get; }
    }
}