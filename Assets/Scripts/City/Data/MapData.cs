using System;
using System.Collections.Generic;

namespace City.Data
{
    [Serializable]
    public class MapData
    {
        public CentralTerrainData CentralTerrain = new();
        public List<TerrainData> Terrains = new();
    }
}