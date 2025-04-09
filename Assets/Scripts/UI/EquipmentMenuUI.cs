using Bellseboss;
using Items;
using Items.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

public class EquipmentMenuUI : MonoBehaviour, IUIScreen
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private VisualTreeAsset itemTemplate;
    private VisualElement root;
    private VisualElement modal;
    private Button backButton;
    private UIManager uiManager;
    private VisualElement helmetContainer, pantContainer, shoesContainer, chestplateContainer;
    private ScrollView scrollView;

    private void OnEnable()
    {
        if (uiDocument == null)
        {
            Debug.LogError("UIDocument no está asignado en el inspector.");
            return;
        }

        root = uiDocument.rootVisualElement;

        if (root == null)
        {
            Debug.LogError("No se pudo obtener el rootVisualElement del UIDocument.");
            return;
        }

        modal = root.Q<VisualElement>("Modal");

        modal.style.display = DisplayStyle.None;

        backButton = root.Q<Button>("Button_Back");
        helmetContainer = root.Q<VisualElement>("Helmet");
        pantContainer = root.Q<VisualElement>("Pants");
        shoesContainer = root.Q<VisualElement>("Shoes");
        chestplateContainer = root.Q<VisualElement>("Chestplate");
        scrollView = root.Q<ScrollView>("ItemsContainer");

        helmetContainer.RegisterCallback<PointerDownEvent>(ClickPointDownHelmet);
        helmetContainer.RegisterCallback<PointerUpEvent>(evt => helmetContainer.RemoveFromClassList("active"));
        pantContainer.RegisterCallback<PointerDownEvent>(ClickPointDownPants);
        pantContainer.RegisterCallback<PointerUpEvent>(evt => pantContainer.RemoveFromClassList("active"));
        shoesContainer.RegisterCallback<PointerDownEvent>(ClickPointDownShoes);
        shoesContainer.RegisterCallback<PointerUpEvent>(evt => shoesContainer.RemoveFromClassList("active"));
        chestplateContainer.RegisterCallback<PointerDownEvent>(ClickPointDownChest);
        chestplateContainer.RegisterCallback<PointerUpEvent>(evt => chestplateContainer.RemoveFromClassList("active"));

        backButton.clicked += OnBackButtonClicked;
    }

    private void ClickPointDownHelmet(PointerDownEvent evt)
    {
        helmetContainer.AddToClassList("active");
        LoadItems(EquipmentSlot.Helmet);
    }

    private void ClickPointDownPants(PointerDownEvent evt)
    {
        pantContainer.AddToClassList("active");
    }

    private void ClickPointDownShoes(PointerDownEvent evt)
    {
        shoesContainer.AddToClassList("active");
    }

    private void ClickPointDownChest(PointerDownEvent evt)
    {
        chestplateContainer.AddToClassList("active");
    }

    private void OnDisable()
    {
        if (backButton != null)
        {
            backButton.clicked -= OnBackButtonClicked;
        }

        helmetContainer?.UnregisterCallback<PointerDownEvent>(evt => helmetContainer.AddToClassList("active"));
        helmetContainer?.UnregisterCallback<PointerUpEvent>(evt => helmetContainer.RemoveFromClassList("active"));
        pantContainer?.UnregisterCallback<PointerDownEvent>(evt => pantContainer.AddToClassList("active"));
        pantContainer?.UnregisterCallback<PointerUpEvent>(evt => pantContainer.RemoveFromClassList("active"));
        shoesContainer?.UnregisterCallback<PointerDownEvent>(evt => shoesContainer.AddToClassList("active"));
        shoesContainer?.UnregisterCallback<PointerUpEvent>(evt => shoesContainer.RemoveFromClassList("active"));
        chestplateContainer?.UnregisterCallback<PointerDownEvent>(evt => chestplateContainer.AddToClassList("active"));
        chestplateContainer?.UnregisterCallback<PointerUpEvent>(
            evt => chestplateContainer.RemoveFromClassList("active"));
    }

    public void Initialize(UIManager manager)
    {
        uiManager = manager;
        uiManager.RegisterScreen("EquipmentMenu", this);
    }

    private void LoadItems(EquipmentSlot typeOfSlot)
    {
        // Cargar los items del jugador
        scrollView.Clear();
        var items = ServiceLocator.Instance.GetService<IDataBaseService>().GetItems();
        foreach (var item in items)
        {
            if (item.Data.lootType != LootType.Equipable || item.Data.equipmentSlot == EquipmentSlot.NONE
                // || item.Slot != typeOfSlot
                ) continue;
            var itemElement = itemTemplate.Instantiate();
            itemElement.Q<Label>("ItemName").text = item.Data.itemName;
            itemElement.Q<Label>("ItemDescription").text = item.Data.baseStats.ToString();
            itemElement.RegisterCallback<PointerUpEvent>(evt => { EquipItem(item); });
            //itemElement.Q<VisualElement>("ItemImage").style.backgroundImage = SpriteToTexture2D(item.sprite);
            scrollView.Add(itemElement);
        }

        modal.style.display = DisplayStyle.Flex;
    }

    private void EquipItem(LootItemInstance item)
    {
        modal.style.display = DisplayStyle.None;
        uiManager.EquipItem(item);
        switch (item.Data.equipmentSlot)
        {
            case EquipmentSlot.Helmet:
                helmetContainer.style.backgroundColor = new Color(0.5f, 0.5f, 0.5f);
                // helmetContainer.Q<Label>("ItemName").text = item.itemName;
                // helmetContainer.Q<Label>("ItemDescription").text = item.stats.ToString();
                break;
            case EquipmentSlot.Pants:
                pantContainer.style.backgroundColor = new Color(0.5f, 0.5f, 0.5f);
                // pantContainer.Q<Label>("ItemName").text = item.itemName;
                // pantContainer.Q<Label>("ItemDescription").text = item.stats.ToString();
                break;
            case EquipmentSlot.Shoes:
                shoesContainer.style.backgroundColor = new Color(0.5f, 0.5f, 0.5f);
                // shoesContainer.Q<Label>("ItemName").text = item.itemName;
                // shoesContainer.Q<Label>("ItemDescription").text = item.stats.ToString();
                break;
            case EquipmentSlot.Chestplate:
                chestplateContainer.style.backgroundColor = new Color(0.5f, 0.5f, 0.5f);
                // chestplateContainer.Q<Label>("ItemName").text = item.itemName;
                // chestplateContainer.Q<Label>("ItemDescription").text = item.stats.ToString();
                break;
        }
    }

    private void OnBackButtonClicked()
    {
        uiManager?.ShowScreen("MainMenu");
    }

    public void Show() => root.style.display = DisplayStyle.Flex;
    public void Hide() => root.style.display = DisplayStyle.None;

    private Texture2D SpriteToTexture2D(Sprite sprite)
    {
        if (sprite == null) return null;

        // Crea una nueva textura con el tamaño del sprite
        Texture2D texture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);

        // Copia los píxeles del sprite a la textura
        Color[] pixels = sprite.texture.GetPixels(
            (int)sprite.rect.x,
            (int)sprite.rect.y,
            (int)sprite.rect.width,
            (int)sprite.rect.height
        );

        texture.SetPixels(pixels);
        texture.Apply();

        return texture;
    }
}