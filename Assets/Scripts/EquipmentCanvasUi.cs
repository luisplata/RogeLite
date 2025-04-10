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
        modal.Initialize(this);
        UpdateImages();
    }

    private void UpdateImages()
    {
        //Load Data from service
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