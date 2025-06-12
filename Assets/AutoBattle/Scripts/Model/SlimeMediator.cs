using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using Items;
using Items.Runtime;
using UnityEngine;
using Random = UnityEngine.Random;

public class SlimeMediator : MonoBehaviour, IDamageable, IAttacker, ILevel
{
    [SerializeField] protected SlimeCombatStats slimeStats;
    [SerializeField] private string slimeName;
    public LootItem equippedWeapon;
    [SerializeField] protected LootItemInstance equippedWeaponInstance;
    public SlimeClassSO slimeClass;
    public bool IsPlayerTeam => slimeStats.isPlayerTeam;
    public bool IsAlive => slimeStats.IsAlive;
    public float CurrentHp => slimeStats.CurrentHp;
    public string SlimeName => slimeStats.SlimeName;
    public float Attack => slimeStats.Attack;
    public event Action OnDeath;

    private void Awake()
    {
        Configure();
    }

    public void Configure()
    {
        slimeStats.Configure(slimeClass, slimeName, this);
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
        Debug.Log($"{attacker.SlimeName} está atacando a posibles {turnOrder.Count} enemigos.");
        var enemies = turnOrder;
        if (enemies.Count == 0) yield break;

        var target = GetTarget(enemies);
        yield return StartCoroutine(attacker.PlayAttackAnimation(attacker, target));
        var weaponBonus = WeaponTriangleSystem.GetWeaponTriangleBonus(attacker.equippedWeaponInstance.WeaponType,
            target.equippedWeaponInstance.WeaponType);
        if (target is IDamageable damageableTarget)
        {
            damageableTarget.TakeDamage((int)(attacker.Attack + weaponBonus), this);
        }
        else
        {
            Debug.LogWarning($"{target.SlimeName} no implementa IDamageable, no se puede aplicar daño.");
        }

        if (target.CurrentHp <= 0)
        {
            Debug.Log($"{target.SlimeName} ha sido derrotado.");
            if (target is IXPSource xpSource)
            {
                int expGain = xpSource.GetXPAmount();
                Debug.Log($"{attacker.SlimeName} gana {expGain} de experiencia por derrotar a {target.SlimeName}.");
                slimeStats.GainExp(expGain);
            }
        }

        yield return new WaitForSeconds(0.5f);
    }

    private static SlimeMediator GetTarget(List<SlimeMediator> enemies)
    {
        //TODO Extract to a TargetSelector class from class?
        var target = enemies[Random.Range(0, enemies.Count)];
        return target;
    }


    private int GetExpGain()
    {
        return slimeStats.BaseGainExp;
    }

    public void TakeDamage(int amount, IAttacker attacker)
    {
        slimeStats.TakeDamage(amount);
        if (!slimeStats.IsAlive)
        {
            Die(attacker);
        }
    }

    private void Die(IAttacker attacker)
    {
        OnDeath?.Invoke();
        attacker.OnKill(this);
        //Destroy(gameObject);
        slimeStats.Die();
    }

    public void OnKill(IDamageable target)
    {
        if (target is IXPSource xpSource) GainXP(xpSource.GetXPAmount());
        if (target is ILootable lootable) GetItems(lootable.GetLoot());
        if (target is IGoldLootable goldLootable) GetGold(goldLootable.GetGold());
    }

    private void GainXP(int amount) => slimeStats.GainExp(amount);

    private void GetItems(List<LootItemInstance> loot)
    {
        foreach (var lootItemInstance in loot)
        {
            Debug.Log($"Looted {lootItemInstance.LootItemConfig.ItemName} with {lootItemInstance.Stars} stars");
            ServiceLocator.Instance.GetService<IInventoryService>().AddItem(lootItemInstance);
        }
    }

    private void GetGold(int gold)
    {
        //TODO: Implementar lógica para obtener oro
        Debug.Log($"{SlimeName} obtiene {gold} de oro.");
    }

    public void SetLevel(int level)
    {
        slimeStats.SetLevel(level);
    }
}