using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Items;
using Items.Runtime;
using UnityEngine;

public class SlimeMediator : MonoBehaviour
{
    [SerializeField] private SlimeCombatStats slimeStats;
    [SerializeField] private string slimeName;
    public LootItem equippedWeapon;
    [SerializeField] private LootItemInstance equippedWeaponInstance;
    public SlimeClassSO slimeClass;
    public bool IsPlayerTeam => slimeStats.isPlayerTeam;
    public bool IsAlive => slimeStats.IsAlive;
    public float CurrentHp => slimeStats.CurrentHp;
    public string SlimeName => slimeStats.SlimeName;
    public float Attack => slimeStats.Attack;

    private void Awake()
    {
        Configure();
    }

    public void Configure()
    {
        slimeStats.Configure(slimeClass, slimeName);
        if (equippedWeapon != null)
        {
            equippedWeaponInstance = new LootItemInstance(equippedWeapon, 1);
        }
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


    public IEnumerator PerformAction(SlimeMediator attacker, List<SlimeMediator> turnOrder)
    {
        var enemies = turnOrder.Where(s => s.IsAlive && s.IsPlayerTeam != attacker.IsPlayerTeam).ToList();
        if (enemies.Count == 0) yield break;

        var target = enemies[Random.Range(0, enemies.Count)];
        yield return StartCoroutine(attacker.PlayAttackAnimation(attacker, target));
        var weaponBonus = WeaponTriangleSystem.GetWeaponTriangleBonus(attacker.equippedWeaponInstance.WeaponType,
            target.equippedWeaponInstance.WeaponType);
        Debug.Log(
            $"{attacker.SlimeName} ataca a {target.SlimeName} con {attacker.equippedWeaponInstance.LootItemConfig.ItemName} contra {target.equippedWeaponInstance.WeaponType}. Bonus de triangulo de armas: {weaponBonus}");
        target.TakeDamage(attacker.Attack + weaponBonus);
        Debug.Log($"{attacker.SlimeName} ataca a {target.SlimeName}. HP restante: {target.CurrentHp}");

        if (target.CurrentHp <= 0)
        {
            Debug.Log($"{target.SlimeName} ha sido derrotado.");
            slimeStats.GainExp(target.GetExpGain());
        }

        yield return new WaitForSeconds(0.5f);
    }


    private int GetExpGain()
    {
        return slimeStats.BaseGainExp;
    }
}