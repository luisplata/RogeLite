using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace City
{
    public class BuyTerrains : MonoBehaviour
    {
        [SerializeField] private Button _buyButton;
        [SerializeField] private RulesInCity _rulesInCity;
        private bool modeBuy;

        private void Start()
        {
            _buyButton.onClick.AddListener(OnBuyButtonClicked);
        }

        private void OnBuyButtonClicked()
        {
            modeBuy = !modeBuy;
            _buyButton.GetComponentInChildren<TextMeshProUGUI>().text = modeBuy ? "Cancel" : "Buy";
            _rulesInCity.SetMode(modeBuy);
        }
    }
}