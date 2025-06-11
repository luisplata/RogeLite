using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bellseboss;
using UnityEngine;
using Bellseboss.States;

public class CombatManager : MonoBehaviour, ICombatManagerTurn, ICombatManagerCheck, ICombatManagerResult
{
    public List<SlimeMediator> turnOrder;
    public int currentTurn = 0;
    public bool PlayerWon => !turnOrder.Any(s => s.IsAlive && !s.IsPlayerTeam);
    private EnemyStatesConfiguration _enemyStatesConfiguration;
    private bool _initialized;

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

    public void Configure()
    {
        _enemyStatesConfiguration = new EnemyStatesConfiguration();
        _enemyStatesConfiguration.AddInitialState(EnemyStatesConfiguration.BeggingState, new BeggingState());
        _enemyStatesConfiguration.AddState(EnemyStatesConfiguration.TurnState, new TurnState(this));
        _enemyStatesConfiguration.AddState(EnemyStatesConfiguration.CheckState, new CheckState(this));
        _enemyStatesConfiguration.AddState(EnemyStatesConfiguration.FinalState, new FinishState(this));
        _initialized = true;
    }

    private void OnDisable()
    {
        _initialized = false;
    }

    public IEnumerator CombatLoop()
    {
        Debug.Log("CombatLoop");
        yield return StartCoroutine(StartState(_enemyStatesConfiguration.GetInitialState()));
        //Debug.Log($"CombatLoop iniciado. Turno actual: {currentTurn} {!IsBattleOver()}");
        // while (!IsBattleOver())
        // {
        //     //Debug.Log($"CombatLoop iniciado. Turno actual: {currentTurn} {!IsBattleOver()}");
        //     var slime = turnOrder[currentTurn];
        //     if (slime.IsAlive)
        //     {
        //         yield return StartCoroutine(slime.PerformAction(slime, turnOrder));
        //     }
        //
        //     currentTurn = (currentTurn + 1) % turnOrder.Count;
        //     yield return new WaitForSeconds(0.5f);
        // }
        //
        // //Debug.Log($"¡Batalla terminada! ¿Ganó el jugador?: {PlayerWon}");
        // yield return new WaitForSeconds(1f);
    }

    public bool IsBattleOver()
    {
        return !turnOrder.Any(s => s.IsAlive && s.IsPlayerTeam) ||
               !turnOrder.Any(s => s.IsAlive && !s.IsPlayerTeam);
    }

    public void ResetSlimes()
    {
        foreach (var slime in turnOrder)
        {
            slime.Configure();
        }

        currentTurn = 0;
    }

    public SlimeMediator GetNextSlime()
    {
        var slime = turnOrder[currentTurn];
        currentTurn = (currentTurn + 1) % turnOrder.Count;
        return slime;
    }

    public List<SlimeMediator> AllSlimes()
    {
        return turnOrder;
    }

    public IEnumerator Coroutine(IEnumerator performAction)
    {
        if (performAction == null)
        {
            Debug.LogError("Coroutine action is null, cannot start coroutine.");
            yield break;
        }

        yield return StartCoroutine(performAction);
    }

    public string GetResult()
    {
        return PlayerWon
            ? "¡El jugador ha ganado la batalla!"
            : "Los enemigos han derrotado al jugador.";
    }
}

public interface ICombatManagerResult
{
    string GetResult();
}

public interface ICombatManagerCheck
{
    bool IsBattleOver();
}

public interface ICombatManagerTurn
{
    SlimeMediator GetNextSlime();
    List<SlimeMediator> AllSlimes();
    IEnumerator Coroutine(IEnumerator performAction);
}