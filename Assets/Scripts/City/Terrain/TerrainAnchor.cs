using UnityEngine;

namespace City.Terrain
{
    public class TerrainAnchor : MonoBehaviour
    {
        [Header("Data")]
        public string Id; // "N", "S", "E", "O", etc.
        public bool IsOccupied { get; private set; }
        public TerrainAnchor ConnectedAnchor { get; private set; }
        public BaseTerrain OwnerTerrain { get; private set; }

        [Header("Optional")]
        [SerializeField] private Collider _collider;
        
        [SerializeField] private MeshRenderer visual;

        public void Configure(BaseTerrain ownerTerrain)
        {
            OwnerTerrain = ownerTerrain;
            IsOccupied = false;
            ConnectedAnchor = null;
            EnableCollider(false);
        }

        public void ConnectTo(TerrainAnchor targetAnchor)
        {
            ConnectedAnchor = targetAnchor;
            IsOccupied = true;
            EnableCollider(false);
        }

        public void Disconnect()
        {
            ConnectedAnchor = null;
            IsOccupied = false;
        }

        public void EnableCollider(bool enabled)
        {
            if (_collider != null)
            {
                _collider.enabled = enabled;
                visual.enabled = enabled;
            }
        }
    }
}