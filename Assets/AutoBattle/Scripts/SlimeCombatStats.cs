using System.Collections;
using UnityEngine;

public class SlimeCombatStats : MonoBehaviour
{
    private string slimeName;
    private SlimeClassSO slimeClass;

    [SerializeField] private int level = 1;
    [SerializeField] private int currentExp = 0;
    private int maxExp = 100;

    private int currentHP;
    private int maxHP;
    private int attack;
    private int defense;
    private int speed;

    public bool isPlayerTeam;

    [SerializeField] private SlimeCombatVisual visual;

    public bool IsAlive => currentHP > 0;
    public string SlimeName => slimeName;
    public float CurrentHP => currentHP;
    public float MaxHP => maxHP;
    public float Attack => attack;

    void Start()
    {
        InitializeStats();
        visual.Configure(this);
    }

    public void InitializeStats()
    {
        maxHP = slimeClass.baseHP;
        attack = slimeClass.baseAttack;
        defense = slimeClass.baseDefense;
        speed = slimeClass.baseSpeed;
        currentHP = maxHP;
    }

    public void GainExp(int amount)
    {
        currentExp += amount;
        while (currentExp >= maxExp)
        {
            currentExp -= maxExp;
            LevelUp();
        }
    }

    void LevelUp()
    {
        level++;

        // Cada stat sube según su tasa de crecimiento
        if (Random.value < slimeClass.hpGrowth) maxHP += 1;
        if (Random.value < slimeClass.attackGrowth) attack += 1;
        if (Random.value < slimeClass.defenseGrowth) defense += 1;
        if (Random.value < slimeClass.speedGrowth) speed += 1;

        currentHP = maxHP;
        Debug.Log($"{slimeName} subió a nivel {level}!");
    }

    public IEnumerator PlayAttackAnimation(SlimeMediator attacker, SlimeMediator target)
    {
        yield return StartCoroutine(visual.MoveToTargetAndBack(attacker.transform, target.transform.position));
        yield return StartCoroutine(visual.HurtFlashEffect(target));
    }

    public void Configure(SlimeClassSO slimeClassSo)
    {
        slimeClass = slimeClassSo;
        slimeName = slimeClass.className;

        // Inicializar stats basados en la clase de slime
        maxHP = slimeClass.baseHP;
        attack = slimeClass.baseAttack;
        defense = slimeClass.baseDefense;
        speed = slimeClass.baseSpeed;

        currentHP = maxHP;

        // Actualizar visualización
        if (visual != null)
        {
            visual.Configure(this);
        }
    }

    public void TakeDamage(float attackerAttack)
    {
        float damage = Mathf.Max(0, attackerAttack - defense);
        currentHP -= (int)damage;

        if (currentHP < 0) currentHP = 0;

        Debug.Log($"{slimeName} de {attackerAttack} recibió {damage} de daño. HP restante: {currentHP}");
    }
}