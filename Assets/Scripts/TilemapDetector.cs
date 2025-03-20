using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapDetector : MonoBehaviour
{
    public Tilemap[] tilemaps; // Arrastra aquí los Tilemaps desde el Inspector

    private void Start()
    {
        tilemaps = FindObjectsByType<Tilemap>(FindObjectsSortMode.None).ToArray();
    }

    private void FixedUpdate()
    {
        foreach (Tilemap tilemap in tilemaps)
        {
            Vector3Int cellPosition = tilemap.WorldToCell(transform.position);

            if (tilemap.HasTile(cellPosition)) // Verifica si hay un tile en esta celda
            {
                TilemapSurface surface = tilemap.GetComponent<TilemapSurface>();

                if (surface != null)
                {
                    //Debug.Log("Superficie detectada: " + surface.surfaceType);
                    //TODO: Send to player in what tile is him ground
                }
            }
        }
    }
}