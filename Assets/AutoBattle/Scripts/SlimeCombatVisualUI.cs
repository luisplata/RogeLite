using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlimeCombatVisualUI : MonoBehaviour
{
    public TextMeshProUGUI slimeNameText;

    public Image healthFillImage;
    private SlimeCombatStats _slimeStats;

    public void Configure(SlimeCombatStats slimeStats)
    {
        _slimeStats = slimeStats;
        
        slimeNameText.text = slimeStats.SlimeName;
    }

    void Update()
    {
        if (_slimeStats != null && healthFillImage != null)
        {
            float fillAmount = (float)_slimeStats.CurrentHP / _slimeStats.MaxHP;
            healthFillImage.fillAmount = Mathf.Clamp01(fillAmount);
        }
    }
}