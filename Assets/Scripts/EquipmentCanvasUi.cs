using Bellseboss;
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
        helmetButton.onClick.AddListener(() => { OpenEquipmentModal(EquipmentSlot.Helmet); });
        pantsButton.onClick.AddListener(() => { OpenEquipmentModal(EquipmentSlot.Pants); });
        shoesButton.onClick.AddListener(() => { OpenEquipmentModal(EquipmentSlot.Shoes); });
        chestplateButton.onClick.AddListener(() => { OpenEquipmentModal(EquipmentSlot.Chestplate); });
        modal.Initialize(this);
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

    private void OpenEquipmentModal(EquipmentSlot slot)
    {
        modal.Show(slot);
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

    public void EquipItem(LootItemInstance item)
    {
        //TODO implement player stats service to apply stats in game
        ServiceLocator.Instance.GetService<IPlayerConfigurationService>().EquipItem(item);
        modal.Hide();
        UpdateImages();
    }
}