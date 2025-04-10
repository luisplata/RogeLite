using UnityEngine;

namespace Inventory
{
    public class GoldGenerationService : IGoldGenerationService
    {
        public int GenerateGold(int baseGold, float luckFactor, int stars)
        {
            int minGold = baseGold;
            float starMultiplier = 1 + (stars - 1) * 0.2f; // Generación de oro basada en las estrellas
            float luckMultiplier = Random.Range(0.8f, 1.2f); // Generación de oro basada en suerte

            // Genera el oro, y aseguramos que no sea menos que el oro base
            int generatedGold = Mathf.FloorToInt(minGold * starMultiplier * luckMultiplier);
            return Mathf.Max(generatedGold, baseGold); // Aseguramos que el oro mínimo no sea menor que el base
        }
    }
}