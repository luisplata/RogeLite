using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour {
    public List<SlimeCombatStats> turnOrder;
    private int currentTurn = 0;

    void Start() {
        StartCoroutine(CombatLoop());
    }

    IEnumerator CombatLoop() {
        while (!IsBattleOver()) {
            var slime = turnOrder[currentTurn];
            if (slime.IsAlive) {
                yield return slime.StartCoroutine(PerformAction(slime));
            }

            currentTurn = (currentTurn + 1) % turnOrder.Count;
            yield return new WaitForSeconds(0.5f);
        }

        Debug.Log("¡Batalla terminada!");
    }

    IEnumerator PerformAction(SlimeCombatStats attacker) {
        var enemies = turnOrder.Where(s => s.IsAlive && s.isPlayerTeam != attacker.isPlayerTeam).ToList();
        if (enemies.Count == 0) yield break;

        var target = enemies[Random.Range(0, enemies.Count)];
        yield return StartCoroutine(attacker.PlayAttackAnimation(attacker, target));
        target.currentHP -= attacker.attack;
        Debug.Log($"{attacker.slimeName} ataca a {target.slimeName}. HP restante: {target.currentHP}");

        if (target.currentHP <= 0) {
            Debug.Log($"{target.slimeName} ha sido derrotado.");
        }

        yield return new WaitForSeconds(0.5f);
    }

    bool IsBattleOver() {
        return !turnOrder.Any(s => s.IsAlive && s.isPlayerTeam) ||
               !turnOrder.Any(s => s.IsAlive && !s.isPlayerTeam);
    }
}