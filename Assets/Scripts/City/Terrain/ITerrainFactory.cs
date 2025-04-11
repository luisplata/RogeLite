namespace City.Terrain
{
    public interface ITerrainFactory
    {
        BaseTerrain CreateTerrain(string id);
    }
}