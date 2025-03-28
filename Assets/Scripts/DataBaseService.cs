using System.Collections.Generic;
using UnityEngine;
using Bellseboss;

public class DataBaseService : MonoBehaviour, IDataBaseService
{
    private void Awake()
    {
        var count = FindObjectsByType<DataBaseService>(FindObjectsSortMode.None).Length;
        if (count > 1)
        {
            Destroy(gameObject);
            return;
        }

        ServiceLocator.Instance.RegisterService<IDataBaseService>(this);
        DontDestroyOnLoad(gameObject);

        LoadInventory();
    }

    public void AddItem(Item item)
    {
        // Cargar el inventario actual
        InventoryData inventory = LoadInventory();

        // Agregar el nuevo ítem
        inventory.Items.Add(item);

        // Convertir a JSON y guardar
        string json = JsonUtility.ToJson(inventory);
        PlayerPrefs.SetString("Inventory", json);
        PlayerPrefs.Save(); // Asegurar que se guarde inmediatamente

        Debug.Log($"Json Saved: {json}");
    }

    private InventoryData LoadInventory()
    {
        string json = PlayerPrefs.GetString("Inventory", "{}"); // Evitar null
        Debug.Log($"Json Loaded: {json}");

        InventoryData inventory = JsonUtility.FromJson<InventoryData>(json);
        return inventory;
    }

    public List<Item> GetItems()
    {
        return new List<Item>(LoadInventory().Items);
    }

    public void ClearItems()
    {
        PlayerPrefs.DeleteKey("Inventory"); // Elimina los datos guardados
        PlayerPrefs.Save(); // Asegurar que se apliquen los cambios
    }

    public void SaveInventory(Inventory inventory)
    {
        foreach (var item in inventory.GetAllItems())
        {
            AddItem(item);
        }
    }
}