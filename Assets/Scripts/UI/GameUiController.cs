using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUiController : MonoBehaviour
{
    private Label levelLabel, experience, health, damage, cooldown, speed, gold;

    private void Start()
    {
        var uiDocument = GetComponent<UIDocument>();

        var root = uiDocument.rootVisualElement;

        levelLabel = root.Q<Label>("Nivel");
        experience = root.Q<Label>("Exp");
        health = root.Q<Label>("Health");
        damage = root.Q<Label>("Damage");
        cooldown = root.Q<Label>("CooldDown");
        speed = root.Q<Label>("Speed");
        gold = root.Q<Label>("Gold");

        ServiceLocator.Instance.GetService<IGameUiController>().OnUpdate += OnOnUpdate;
    }

    private void OnOnUpdate(PlayerStats playerStats)
    {
        levelLabel.text = $"Level: {playerStats.Level}";
        experience.text = $"exp: {playerStats.GetExp()}";
        health.text = $"Health: {playerStats.Health}";
        damage.text = $"Damage: {playerStats.Damage}";
        cooldown.text = $"AttackCooldown: {playerStats.AttackCooldown}";
        speed.text = $"MoveSpeed: {playerStats.MoveSpeed}";
        gold.text = $"Gold: {playerStats.Gold}";
    }
}