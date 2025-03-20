using System.Collections.Generic;
using UnityEngine;
using Bellseboss;

public class DataBaseService : MonoBehaviour, IDataBaseService
{
    private List<Item> obtainedItems = new();

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
    }

    public void AddItem(Item item)
    {
        obtainedItems.Add(item);
        Debug.Log($"Item almacenado: {item.Name} ({item.Stars}★)");
    }

    public List<Item> GetItems()
    {
        return new List<Item>(obtainedItems); // Se retorna una copia para evitar modificaciones externas.
    }

    public void ClearItems()
    {
        obtainedItems.Clear();
        Debug.Log("Base de datos temporal limpiada.");
    }
}