using System;

namespace Inventory
{
    public interface IGoldGenerationService
    {
        int GenerateGold(int baseGold, float luckFactor, int stars);
    }
}