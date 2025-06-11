using System;
using System.Collections;
using Bellseboss.States;
using UnityEngine;

namespace Bellseboss
{
    public class BattleStatesMachine : MonoBehaviour
    {
        private EnemyStatesConfiguration _enemyStatesConfiguration;
        private bool _initialized;

        private void Awake()
        {
            Configure();
        }

        private void Configure()
        {
            _enemyStatesConfiguration = new EnemyStatesConfiguration();
            _enemyStatesConfiguration.AddInitialState(EnemyStatesConfiguration.BeggingState, new BeggingState());
            _enemyStatesConfiguration.AddState(EnemyStatesConfiguration.TurnState, new TurnState());
            _enemyStatesConfiguration.AddState(EnemyStatesConfiguration.CheckState, new CheckState());
            _enemyStatesConfiguration.AddState(EnemyStatesConfiguration.FinalState, new FinishState());
        }

        private void Start()
        {
            StartCoroutine(StartState(_enemyStatesConfiguration.GetInitialState()));
        }

        private void OnEnable()
        {
            _initialized = true;
        }

        private void OnDisable()
        {
            _initialized = false;
        }

        private IEnumerator StartState(IBattleState state)
        {
            if (state == null)
            {
                Debug.LogError("State is null, cannot start state machine.");
                yield break;
            }

            while (_initialized)
            {
                yield return state.DoEnter();
                yield return state.DoAction();
                yield return state.DoExit();
                if (state.NextStateId == EnemyStatesConfiguration.FinalState)
                {
                    Debug.Log("Battle finished.");
                    break;
                }

                state = _enemyStatesConfiguration.GetState(state.NextStateId);
            }
        }
    }
}