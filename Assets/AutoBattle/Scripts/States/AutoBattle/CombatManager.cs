using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public List<SlimeMediator> turnOrder;
    public int currentTurn = 0;
    public bool PlayerWon => !turnOrder.Any(s => s.IsAlive && !s.IsPlayerTeam);

    public IEnumerator CombatLoop()
    {
        //Debug.Log($"CombatLoop iniciado. Turno actual: {currentTurn} {!IsBattleOver()}");
        while (!IsBattleOver())
        {
            //Debug.Log($"CombatLoop iniciado. Turno actual: {currentTurn} {!IsBattleOver()}");
            var slime = turnOrder[currentTurn];
            if (slime.IsAlive)
            {
                yield return StartCoroutine(slime.PerformAction(slime, turnOrder));
            }

            currentTurn = (currentTurn + 1) % turnOrder.Count;
            yield return new WaitForSeconds(0.5f);
        }

        //Debug.Log($"¡Batalla terminada! ¿Ganó el jugador?: {PlayerWon}");
        yield return new WaitForSeconds(1f);
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
}