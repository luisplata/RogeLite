using System;
using UnityEngine;

public class XPManager
{
    private XPConfig config;
    private int currentXP;
    private int currentLevel;

    public event Action<int> OnLevelUp; // Evento para cuando se sube de nivel.

    public XPManager(XPConfig config)
    {
        this.config = config;
        currentXP = 0;
        currentLevel = 1;
    }

    public int GetCurrentLevel() => currentLevel;
    public int GetCurrentXP() => currentXP;
    public int GetXPToNextLevel() => Mathf.RoundToInt(config.baseXP * Mathf.Pow(currentLevel, config.xpFactor));

    public void AddXP(int amount)
    {
        currentXP += amount;
        while (currentXP >= GetXPToNextLevel() && currentLevel < config.maxLevel)
        {
            currentXP -= GetXPToNextLevel();
            currentLevel++;
            OnLevelUp?.Invoke(currentLevel); // Disparar evento de subida de nivel
        }
    }
}