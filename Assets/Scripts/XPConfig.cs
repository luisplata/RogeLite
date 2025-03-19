using UnityEngine;

[CreateAssetMenu(fileName = "XPConfig", menuName = "XP System/Config")]
public class XPConfig : ScriptableObject
{
    public int baseXP = 100;
    public float xpFactor = 1.5f;
    public int maxLevel = 50;
}