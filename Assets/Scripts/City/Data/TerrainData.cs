using System;

namespace City.Data
{
    [Serializable]
    public class TerrainData
    {
        public string Id;
        public string Anchor;     // Ej: "N", "S", "E", "O"
        public string ConnectTo;  // Ej: "S", "N", "E", "O"
    }
}