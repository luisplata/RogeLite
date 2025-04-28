using System;

namespace City.Data
{
    [Serializable]
    public class TerrainData
    {
        public string Id;
        public string ParentGuid; // <<< Nuevo
        public string Anchor; // Nuestro anchor
        public string ConnectTo; // Anchor del terreno padre
        public string Guid;
    }
}