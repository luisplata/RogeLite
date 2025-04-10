using Items;
using Items.Equipment;
using Items.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentCanvasUi : MonoBehaviour, IUIScreen
{
    [SerializeField] private GameObject root;
    [SerializeField] private EquipmentCanvasUiModal modal;
    [SerializeField] private Button helmetButton, pantsButton, shoesButton, chestplateButton;
    [SerializeField] private Image[] helmetImage, pantsImage, shoesImage, chestplateImage;
    [SerializeField] private Button backButton;
    [SerializeField] private Sprite defaultSprite;
    private UIManager _uiManager;

    public void Initialize(UIManager uiManager)
    {
        _uiManager = uiManager;
        uiManager.RegisterScreen("EquipmentMenu", this);
        CloseModal();

        backButton.onClick.AddListener(OnBackButtonClicked);
        helmetButton.onClick.AddListener(() => { OpenEquipmentModal(EquipmentSlot.Head); });
        pantsButton.onClick.AddListener(() => { OpenEquipmentModal(EquipmentSlot.Pants); });
        shoesButton.onClick.AddListener(() => { OpenEquipmentModal(EquipmentSlot.Shoes); });
        chestplateButton.onClick.AddListener(() => { OpenEquipmentModal(EquipmentSlot.Chest); });
        modal.Initialize(this);
        UpdateImages();
    }

    private void OpenEquipmentModal(EquipmentSlot slot)
    {
        modal.Show(slot);
    }

    public void EquipItem(LootItemInstance item)
    {
        //TODO implement player stats service to apply stats in game
        ServiceLocator.Instance.GetService<IEquipmentPersistenceService>().EquipItem(item);
        modal.Hide();
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

    private void OnBackButtonClicked()
    {
        _uiManager.ShowScreen("MainMenu");
    }


    public void Show()
    {
        root.SetActive(true);
    }

    public void Hide()
    {
        root.SetActive(false);
    }

    public void CloseModal()
    {
        modal.Hide();
    }
}