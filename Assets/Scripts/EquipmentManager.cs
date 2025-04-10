using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    private PlayerStats playerStats;

    public void Initialize(PlayerStats stats)
    {
        playerStats = stats;
    }
}