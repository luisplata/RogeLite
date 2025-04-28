using System;
using System.Collections.Generic;
using UnityEngine;

namespace City.Terrain
{
    public class BaseTerrain : MonoBehaviour
    {
        [Header("Anchors")]
        [SerializeField] private List<TerrainAnchor> _anchors;

        private readonly Dictionary<string, TerrainAnchor> _anchorsById = new();
        public Guid Guid { get; private set; } = Guid.NewGuid();
    
        public bool IsOwned { get; private set; }

        private void Awake()
        {
            foreach (var anchor in _anchors)
            {
                anchor.Configure(this);
                _anchorsById.Add(anchor.Id, anchor);
            }

            // Si quieres que el GUID persista después de varias partidas, deberías serializarlo manualmente
        }

        public TerrainAnchor GetAnchor(string anchorId)
        {
            return _anchorsById.TryGetValue(anchorId, out var anchor) ? anchor : null;
        }

        public bool IsAnchorOccupied(string anchorId)
        {
            var anchor = GetAnchor(anchorId);
            return anchor != null && anchor.IsOccupied;
        }

        public void OccupyAnchor(string anchorId)
        {
            var anchor = GetAnchor(anchorId);
            anchor?.ConnectTo(null);
        }

        public void SetGuid(Guid parse)
        {
            Guid = parse;
        }

        public string[] GetAllAnchorStates()
        {
            List<string> states = new();
            foreach (var anchor in _anchors)
            {
                states.Add($"{anchor.Id}: {(anchor.IsOccupied ? "Occupied" : "Free")}");
            }
            return states.ToArray();
        }

    }
}