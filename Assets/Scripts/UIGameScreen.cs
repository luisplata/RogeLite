using System;
using TMPro;
using UnityEngine;

public class UIGameScreen : MonoBehaviour, IUIGameScreen
{
    [SerializeField] private TextMeshProUGUI stats, inventory;
    private bool _isConfigured;

    private void Awake()
    {
        var count = FindObjectsByType<UIGameScreen>(FindObjectsSortMode.None).Length;
        if (count > 1)
        {
            Destroy(gameObject);
            return;
        }

        ServiceLocator.Instance.RegisterService<IUIGameScreen>(this);
        _isConfigured = true;
    }

    private void OnDestroy()
    {
        if (_isConfigured)
        {
            ServiceLocator.Instance.UnregisterService<IUIGameScreen>();
        }
    }

    public void SetStatsText(string getFormattedEquipment)
    {
        stats.text = getFormattedEquipment;
    }

    public void SetInventoryText(string getFormattedInventory)
    {
        inventory.text = getFormattedInventory;
    }
}