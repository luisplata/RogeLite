using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public List<SlimeMediator> turnOrder;
    private int currentTurn = 0;

    void Start()
    {
        StartCoroutine(CombatLoop());
    }

    IEnumerator CombatLoop()
    {
        while (!IsBattleOver())
        {
            var slime = turnOrder[currentTurn];
            if (slime.IsAlive)
            {
                yield return slime.StartCoroutine(PerformAction(slime));
            }

            currentTurn = (currentTurn + 1) % turnOrder.Count;
            yield return new WaitForSeconds(0.5f);
        }

        Debug.Log("¡Batalla terminada!");
    }

    IEnumerator PerformAction(SlimeMediator attacker)
    {
        var enemies = turnOrder.Where(s => s.IsAlive && s.IsPlayerTeam != attacker.IsPlayerTeam).ToList();
        if (enemies.Count == 0) yield break;

        var target = enemies[Random.Range(0, enemies.Count)];
        yield return StartCoroutine(attacker.PlayAttackAnimation(attacker, target));
        target.TakeDamage(attacker.Attack);
        Debug.Log($"{attacker.SlimeName} ataca a {target.SlimeName}. HP restante: {target.CurrentHP}");

        if (target.CurrentHP <= 0)
        {
            Debug.Log($"{target.SlimeName} ha sido derrotado.");
        }

        yield return new WaitForSeconds(0.5f);
    }

    bool IsBattleOver()
    {
        return !turnOrder.Any(s => s.IsAlive && s.IsPlayerTeam) ||
               !turnOrder.Any(s => s.IsAlive && !s.IsPlayerTeam);
    }
}