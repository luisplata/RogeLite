using UnityEngine;

[CreateAssetMenu(fileName = "AttackSpeedBoost", menuName = "PowerUps/Attack Speed")]
public class AttackSpeedBoost : PowerUp
{
    public float speedMultiplier = 0.8f; // Reduce el cooldown

    public override void ApplyEffect(PlayerStats playerStats)
    {
        playerStats.IncreaseAttackSpeed(speedMultiplier);
    }
}