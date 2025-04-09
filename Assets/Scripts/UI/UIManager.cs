﻿using System.Collections.Generic;
using Bellseboss;
using Items;
using Items.Runtime;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Dictionary<string, IUIScreen> screens = new();
    private IUIScreen activeScreen;

    public void RegisterScreen(string name, IUIScreen screen)
    {
        if (!screens.ContainsKey(name))
        {
            screens[name] = screen;
            screen.Hide();
        }
    }

    public void ShowScreen(string name)
    {
        if (activeScreen != null)
        {
            activeScreen.Hide();
        }

        if (screens.TryGetValue(name, out IUIScreen screen))
        {
            screen.Show();
            activeScreen = screen;
        }
        else
        {
            Debug.LogError($"UIManager: No se encontró la pantalla {name}.");
        }
    }

    public void EquipItem(LootItemInstance item)
    {
        //TODO implement player stats service to apply stats in game
    }
}

public interface IUIScreen
{
    void Show();
    void Hide();
}