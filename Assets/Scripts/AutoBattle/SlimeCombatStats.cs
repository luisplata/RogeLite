using System.Collections;
using UnityEngine;

public class SlimeCombatStats : MonoBehaviour
{
    public string slimeName;
    public int maxHP;
    public int currentHP;
    public int attack;
    public bool isPlayerTeam;
    public SlimeCombatVisual visual;

    public bool IsAlive => currentHP > 0;

    private void Start()
    {
        currentHP = maxHP;
    }

    public IEnumerator PlayAttackAnimation(SlimeCombatStats attacker, SlimeCombatStats target)
    {
        yield return StartCoroutine(visual.MoveToTargetAndBack(attacker.transform, target.transform.position));
        yield return StartCoroutine(visual.HurtFlashEffect(target));
    }

}