using UnityEngine;

public abstract class PowerUp : ScriptableObject
{
    public string powerUpName;
    public string description;
    public Sprite icon;

    public abstract void ApplyEffect(PlayerStats playerStats);
}