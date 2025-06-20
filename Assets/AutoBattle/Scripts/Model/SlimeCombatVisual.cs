﻿using System.Collections;
using UnityEngine;

public class SlimeCombatVisual : MonoBehaviour
{
    public SlimeCombatVisualUI slimeVisualUI;
    private SlimeCombatStats _slimeStats;

    public void Configure(SlimeCombatStats slimeStats)
    {
        _slimeStats = slimeStats;
        slimeVisualUI.Configure(_slimeStats);
    }

    public IEnumerator HurtFlashEffect(SlimeMediator slime, System.Action onComplete = null)
    {
        SpriteRenderer sr = slime.GetComponent<SpriteRenderer>();
        if (sr == null) yield break;

        Color originalColor = sr.color;
        Color flashColor = Color.red;

        float flashDuration = 0.1f;
        int flashCount = 3;

        for (int i = 0; i < flashCount; i++)
        {
            sr.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            sr.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }

        onComplete?.Invoke();
    }


    public IEnumerator MoveToTargetAndBack(Transform unit, Vector3 targetPosition, float duration = 0.25f)
    {
        SpriteRenderer sr = unit.GetComponent<SpriteRenderer>();
        sr.sortingOrder = 10;
        Vector3 originalPosition = unit.position;
        float t = 0f;

        while (t < 1f)
        {
            unit.position = Vector3.Lerp(originalPosition, targetPosition, t);
            t += Time.deltaTime / duration;
            yield return null;
        }

        t = 0f;
        while (t < 1f)
        {
            unit.position = Vector3.Lerp(targetPosition, originalPosition, t);
            t += Time.deltaTime / duration;
            yield return null;
        }

        sr.sortingOrder = 0;
    }

    public void Died(SlimeMediator slime)
    {
        SpriteRenderer sr = slime.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = Color.gray; // Cambia el color a gris para indicar que está muerto
        }
    }

    public void ResetVisual(SlimeMediator slime)
    {
        SpriteRenderer sr = slime.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = Color.white; // Resetea el color a blanco
        }

        slimeVisualUI.Configure(_slimeStats);
    }
}