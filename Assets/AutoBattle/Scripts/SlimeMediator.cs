using System;
using System.Collections;
using UnityEngine;

public class SlimeMediator : MonoBehaviour
{
    [SerializeField] private SlimeCombatStats slimeStats;
    public SlimeClassSO slimeClass;
    public bool IsPlayerTeam => slimeStats.isPlayerTeam;
    public bool IsAlive => slimeStats.IsAlive;
    public float CurrentHP => slimeStats.CurrentHP;
    public string SlimeName => slimeStats.SlimeName;
    public float Attack => slimeStats.Attack;

    private void Start()
    {
        slimeStats.Configure(slimeClass);
    }

    public void TakeDamage(float attackerAttack)
    {
        if (slimeStats.IsAlive)
        {
            slimeStats.TakeDamage(attackerAttack);
        }
        else
        {
            Debug.Log($"{SlimeName} ya está derrotado y no puede recibir más daño.");
        }
    }

    public IEnumerator PlayAttackAnimation(SlimeMediator attacker, SlimeMediator target)
    {
        yield return slimeStats.PlayAttackAnimation(attacker, target);
    }
}