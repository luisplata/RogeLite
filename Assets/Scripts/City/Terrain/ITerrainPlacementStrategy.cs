using UnityEngine;

namespace City.Terrain
{
    public interface ITerrainPlacementStrategy
    {
        Vector3 CalculatePosition(Transform lastAnchor, Transform currentAnchor);
    }
}