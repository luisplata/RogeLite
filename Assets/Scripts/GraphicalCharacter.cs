using Items;
using Items.Equipment;
using UnityEngine;

public class GraphicalCharacter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] helmetImage, pantsImage, shoesImage, chestplateImage;
    [SerializeField] private Sprite defaultSprite;

    private IGraphicalCharacter graphicalCharacter;

    public void Initialize(IGraphicalCharacter mediator)
    {
        graphicalCharacter = mediator;
        UpdateImages();
    }
private void UpdateImages()
    {
        //Load Data from service
        foreach (var image in helmetImage)
        {
            var itemLoot = ServiceLocator.Instance.GetService<IEquipmentPersistenceService>()
                .GetEquippedItem(EquipmentSlot.Head);
            if (itemLoot == null)
            {
                image.sprite = defaultSprite;
            }
            else
            {
                image.sprite = itemLoot.LootItemConfig.Icon;
            }
        }

        foreach (var image in pantsImage)
        {
            var itemLoot = ServiceLocator.Instance.GetService<IEquipmentPersistenceService>()
                .GetEquippedItem(EquipmentSlot.Pants);
            if (itemLoot == null)
            {
                image.sprite = defaultSprite;
            }
            else
            {
                image.sprite = itemLoot.LootItemConfig.Icon;
            }
        }

        foreach (var image in shoesImage)
        {
            var itemLoot = ServiceLocator.Instance.GetService<IEquipmentPersistenceService>()
                .GetEquippedItem(EquipmentSlot.Shoes);
            if (itemLoot == null)
            {
                image.sprite = defaultSprite;
            }
            else
            {
                image.sprite = itemLoot.LootItemConfig.Icon;
            }
        }

        foreach (var image in chestplateImage)
        {
            var itemLoot = ServiceLocator.Instance.GetService<IEquipmentPersistenceService>()
                .GetEquippedItem(EquipmentSlot.Chest);
            if (itemLoot == null)
            {
                image.sprite = defaultSprite;
            }
            else
            {
                image.sprite = itemLoot.LootItemConfig.Icon;
            }
        }
    }

    public void Hide()
    {
        foreach (var image in helmetImage)
        {
            image.sprite = defaultSprite;
        }

        foreach (var image in pantsImage)
        {
            image.sprite = defaultSprite;
        }

        foreach (var image in shoesImage)
        {
            image.sprite = defaultSprite;
        }

        foreach (var image in chestplateImage)
        {
            image.sprite = defaultSprite;
        }
    }
}