using City.Data;

namespace City
{
    public interface IMapPersistenceService
    {
        void SaveMap(MapData mapData);
        MapData LoadMap();
    }
}