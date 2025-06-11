using System.Collections.Generic;
using Items;
using UnityEngine;

[CreateAssetMenu(fileName = "New Slime Class", menuName = "Slime Class")]
public class SlimeClassSO : ScriptableObject {
    public string className;
    public List<WeaponType> allowedWeapons;
    public int baseHP;
    public int baseAttack;
    public int baseDefense;
    public int baseSpeed;
    public int baseGainExp;

    public float hpGrowth;
    public float attackGrowth;
    public float defenseGrowth;
    public float speedGrowth;
    public float expGrowth;
}