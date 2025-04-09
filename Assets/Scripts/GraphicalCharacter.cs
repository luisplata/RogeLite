using Items;
using UnityEngine;
using UnityEngine.UI;

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
            var itemLoot = ServiceLocator.Instance.GetService<IPlayerConfigurationService>()
                .GetEquippedItem(EquipmentSlot.Helmet);
            if (itemLoot == null)
            {
                image.sprite = defaultSprite;
            }
            else
            {
                image.sprite = itemLoot.Data.itemSprite;
            }
        }

        foreach (var image in pantsImage)
        {
            var itemLoot = ServiceLocator.Instance.GetService<IPlayerConfigurationService>()
                .GetEquippedItem(EquipmentSlot.Pants);
            if (itemLoot == null)
            {
                image.sprite = defaultSprite;
            }
            else
            {
                image.sprite = itemLoot.Data.itemSprite;
            }
        }

        foreach (var image in shoesImage)
        {
            var itemLoot = ServiceLocator.Instance.GetService<IPlayerConfigurationService>()
                .GetEquippedItem(EquipmentSlot.Shoes);
            if (itemLoot == null)
            {
                image.sprite = defaultSprite;
            }
            else
            {
                image.sprite = itemLoot.Data.itemSprite;
            }
        }

        foreach (var image in chestplateImage)
        {
            var itemLoot = ServiceLocator.Instance.GetService<IPlayerConfigurationService>()
                .GetEquippedItem(EquipmentSlot.Chestplate);
            if (itemLoot == null)
            {
                image.sprite = defaultSprite;
            }
            else
            {
                image.sprite = itemLoot.Data.itemSprite;
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