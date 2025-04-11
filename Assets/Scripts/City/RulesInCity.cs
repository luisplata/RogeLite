using System.Collections.Generic;
using UnityEngine;

public class RulesInCity : MonoBehaviour
{
    [Header("Terrain Prefabs")]
    [SerializeField] private List<BaseTerrain> terrainPrefabs;

    // Lista para almacenar los terrenos instanciados
    private List<BaseTerrain> placedTerrains = new List<BaseTerrain>();

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        // JSON con el terreno central y los demás conectados a él
        var terrainDataJson =
            "{\"CentralTerrain\": {\"Id\": \"CentralTerrain\", \"Position\": \"0,0,0\"}, " +
            "\"Terrains\": [" +
            "{\"Id\": \"Grass\", \"Anchor\": \"E\", \"ConnectTo\": \"O\"}, " +
            "{\"Id\": \"Grass\", \"Anchor\": \"N\", \"ConnectTo\": \"S\"}, " +
            "{\"Id\": \"Grass\", \"Anchor\": \"N\", \"ConnectTo\": \"S\"}]}";

        var terrainData = JsonUtility.FromJson<TerrainMapData>(terrainDataJson);
        BaseTerrain centralTerrain = null;
        var centralTerrainInfo = terrainData.CentralTerrain;
        var centralPrefab = GetTerrainPrefabById(centralTerrainInfo.Id);
        if (centralPrefab != null)
        {
            centralTerrain = Instantiate(centralPrefab);
            centralTerrain.transform.position = StringToVector3(centralTerrainInfo.Position);
            placedTerrains.Add(centralTerrain);
        }
        else
        {
            Debug.LogError("Central terrain prefab not found.");
            return;
        }

        // Buscamos el anclaje y la conexión
        var lastTerrain = centralTerrain;
        Vector3 currentPosition = centralTerrain.transform.position;
        // Colocar los demás terrenos conectados al central
        foreach (var terrainInfo in terrainData.Terrains)
        {
            var prefab = GetTerrainPrefabById(terrainInfo.Id);
            if (prefab == null)
            {
                Debug.LogError($"No terrain found with id {terrainInfo.Id}");
                continue;
            }

            BaseTerrain terrain = Instantiate(prefab);

            var lastAnchor = lastTerrain.GetAnchor(terrainInfo.ConnectTo);
            var currentAnchor = terrain.GetAnchor(terrainInfo.Anchor);

            if (lastAnchor != null && currentAnchor != null)
            {
                // Calcular el desplazamiento para que el anclaje del terreno coincida con el anclaje del terreno anterior
                var offset = CalculateOffset(lastAnchor, currentAnchor);

                // Ajustamos la posición del terreno para que el anclaje quede en el lugar adecuado
                currentPosition += offset; // Sumar el desplazamiento acumulativo

                terrain.transform.position = currentPosition;

                // Añadimos el nuevo terreno a la lista
                placedTerrains.Add(terrain);
                lastTerrain = terrain;
            }
            else
            {
                // Si no existe el anclaje de conexión, no se debe crear el terreno
                Debug.LogWarning($"Anclaje no encontrado para conectar el terreno con ID: {terrainInfo.Id}");
            }
        }
    }

    private Vector3 CalculateOffset(Transform lastAnchor, Transform currentAnchor)
    {
        // Definir desplazamientos según los anclajes
        float anchorOffset = 9f; // Distancia entre los anclajes. Debes ajustar esta distancia según tu escala

        // Según los anclajes específicos, calculamos el desplazamiento
        if (currentAnchor.name == "E" && lastAnchor.name == "O")
        {
            // Desplazamiento en el eje X positivo
            return new Vector3(anchorOffset, 0, 0);
        }
        else if (currentAnchor.name == "O" && lastAnchor.name == "E")
        {
            // Desplazamiento en el eje X negativo
            return new Vector3(-anchorOffset, 0, 0);
        }
        else if (currentAnchor.name == "N" && lastAnchor.name == "S")
        {
            // Desplazamiento en el eje Z positivo
            return new Vector3(0, 0, anchorOffset);
        }
        else if (currentAnchor.name == "S" && lastAnchor.name == "N")
        {
            // Desplazamiento en el eje Z negativo
            return new Vector3(0, 0, -anchorOffset);
        }

        return Vector3.zero; // Si no es una conexión válida, no hacemos desplazamiento
    }

    private BaseTerrain GetTerrainPrefabById(string id)
    {
        return terrainPrefabs.Find(t => t.name == id);
    }

    private Vector3 StringToVector3(string position)
    {
        var values = position.Split(',');
        return new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
    }
}

[System.Serializable]
public class TerrainMapData
{
    public TerrainInfo CentralTerrain;
    public TerrainInfo[] Terrains;
}

[System.Serializable]
public class TerrainInfo
{
    public string Id;
    public string Anchor;
    public string ConnectTo;
    public string Position;
}
