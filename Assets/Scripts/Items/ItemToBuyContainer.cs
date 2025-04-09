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
            var totalValue = itemInstance.Stars * itemInstance.Data.priceToBuy;
            var response = await ServiceLocator.Instance.GetService<INotificationService>()
                .ShowDecision($"Sure to buy {itemInstance.Data.itemName} to {totalValue} gold?", NotificationType.Bad);
            if (response)
            {
                ServiceLocator.Instance.GetService<IInventoryService>()
                    .SellItem(itemInstance, totalValue);

                ServiceLocator.Instance.GetService<INotificationService>()
                    .Notify($"Sell! {itemInstance.Data.itemName} by {totalValue}?", NotificationType.Good);

                ServiceLocator.Instance.GetService<IGoldService>().AddGold(totalValue);
            }
            else
            {
                ServiceLocator.Instance.GetService<INotificationService>()
                    .Notify($"Cancel Buy to {itemInstance.Data.itemName}?", NotificationType.Normal);
            }
        }

        public void Configure(LootItemInstance item)
        {
            itemInstance = item;
            // switch (item.Data.lootType)
            // {
            //     case LootType.Consumable:
            //     case LootType.Mineral:
            //         itemImage.color = Color.blue;
            //         break;
            //     case LootType.Equipable:
            //         itemImage.color = Color.yellow;
            //         break;
            //     default:
            //         itemImage.color = Color.white;
            //         break;
            // }

            for (int i = 0; i < item.Stars; i++)
            {
                positionStarts[i].SetActive(true);
            }

            buttonToBuy.onClick.AddListener(TryToSellItem);

            itenName.text = item.Data.itemName;
            itemImage.sprite = item.Data.itemSprite;
        }
    }
}