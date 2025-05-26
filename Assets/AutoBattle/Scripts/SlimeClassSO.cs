using UnityEngine;

[CreateAssetMenu(fileName = "New Slime Class", menuName = "Slime Class")]
public class SlimeClassSO : ScriptableObject {
    public string className;
    public int baseHP;
    public int baseAttack;
    public int baseDefense;
    public int baseSpeed;

    public float hpGrowth;
    public float attackGrowth;
    public float defenseGrowth;
    public float speedGrowth;
}