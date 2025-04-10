using Inventory;
using Items.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    public class ItemToBuyContainer : MonoBehaviour
    {
        [SerializeField] private GameObject[] positionStarts;
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI itenName;
        [SerializeField] private Button buttonToBuy;
        private LootItemInstance itemInstance;

        private async void TryToSellItem()
        {
            var totalValue = itemInstance.Stars * itemInstance.LootItemConfig.SellPrice;
            var response = await ServiceLocator.Instance.GetService<INotificationService>()
                .ShowDecision($"Sure to buy {itemInstance.LootItemConfig.ItemName} to {totalValue} gold?",
                    NotificationType.Bad);
            if (response)
            {
                ServiceLocator.Instance.GetService<IInventoryService>()
                    .SellItem(itemInstance, totalValue);

                ServiceLocator.Instance.GetService<INotificationService>()
                    .Notify($"Sell! {itemInstance.LootItemConfig.ItemName} by {totalValue}?", NotificationType.Good);
                ServiceLocator.Instance.GetService<IPlayerGoldPersistenceService>().AddGold(totalValue);
            }
            else
            {
                ServiceLocator.Instance.GetService<INotificationService>()
                    .Notify($"Cancel Buy to {itemInstance.LootItemConfig.ItemName}?", NotificationType.Normal);
            }
        }

        public void Configure(LootItemInstance item)
        {
            itemInstance = item;
            for (int i = 0; i < item.Stars; i++)
            {
                positionStarts[i].SetActive(true);
            }

            buttonToBuy.onClick.AddListener(TryToSellItem);

            itenName.text = item.LootItemConfig.ItemName;
            itemImage.sprite = item.LootItemConfig.Icon;
        }
    }
}