using Bellseboss;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemToBuyContainer : MonoBehaviour
{
    [SerializeField] private GameObject[] positionStarts;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itenName;
    [SerializeField] private Button buttonToBuy;
    private LootItemInstance itemInstance;

    private async void TryToBuyItem()
    {
        var response = await ServiceLocator.Instance.GetService<INotificationService>()
            .ShowDecision("Sure to buy?", NotificationType.Bad);
        if (response)
        {
            ServiceLocator.Instance.GetService<INotificationService>()
                .Notify("Buy!", NotificationType.Good);
        }
        else
        {
            ServiceLocator.Instance.GetService<INotificationService>()
                .Notify("Cancel Buy", NotificationType.Normal);
        }
    }

    public void Configure(LootItemInstance item)
    {
        itemInstance = item;
        switch (item.itemType)
        {
            case LootType.Consumable:
            case LootType.Mineral:
                itemImage.color = Color.blue;
                break;
            case LootType.Equipable:
                itemImage.color = Color.yellow;
                break;
            default:
                itemImage.color = Color.white;
                break;
        }

        for (int i = 0; i < item.stars; i++)
        {
            positionStarts[i].SetActive(true);
        }
        
        buttonToBuy.onClick.AddListener(TryToBuyItem);

        itenName.text = item.itemName;
    }
}