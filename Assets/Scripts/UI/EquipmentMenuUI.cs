using UnityEngine;
using UnityEngine.UIElements;

public class EquipmentMenuUI : MonoBehaviour, IUIScreen
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private UIDocument modal;
    [SerializeField] private VisualTreeAsset itemTemplate;
    private VisualElement root;
    private VisualElement modalRoot;
    private Button backButton;
    private UIManager uiManager;
    private VisualElement helmetContainer, pantContainer, shoesContainer, chestplateContainer;

    private void OnEnable()
    {
        if (uiDocument == null)
        {
            Debug.LogError("UIDocument no está asignado en el inspector.");
            return;
        }

        root = uiDocument.rootVisualElement;
        modalRoot = modal.rootVisualElement;

        if (root == null)
        {
            Debug.LogError("No se pudo obtener el rootVisualElement del UIDocument.");
            return;
        }

        modalRoot.style.display = DisplayStyle.None;

        backButton = root.Q<Button>("Button_Back");
        helmetContainer = root.Q<VisualElement>("Helmet");
        pantContainer = root.Q<VisualElement>("Pants");
        shoesContainer = root.Q<VisualElement>("Shoes");
        chestplateContainer = root.Q<VisualElement>("Chestplate");

        helmetContainer.RegisterCallback<PointerDownEvent>(evt => helmetContainer.AddToClassList("active"));
        helmetContainer.RegisterCallback<PointerUpEvent>(evt => helmetContainer.RemoveFromClassList("active"));
        pantContainer.RegisterCallback<PointerDownEvent>(evt => pantContainer.AddToClassList("active"));
        pantContainer.RegisterCallback<PointerUpEvent>(evt => pantContainer.RemoveFromClassList("active"));
        shoesContainer.RegisterCallback<PointerDownEvent>(evt => shoesContainer.AddToClassList("active"));
        shoesContainer.RegisterCallback<PointerUpEvent>(evt => shoesContainer.RemoveFromClassList("active"));
        chestplateContainer.RegisterCallback<PointerDownEvent>(evt => chestplateContainer.AddToClassList("active"));
        chestplateContainer.RegisterCallback<PointerUpEvent>(evt => chestplateContainer.RemoveFromClassList("active"));

        backButton.clicked += OnBackButtonClicked;
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
        
        // Cargar los items del jugador
        var items = ServiceLocator.Instance.GetService<DataBaseService>().GetItems();
        foreach (var item in items)
        {
            var itemElement = itemTemplate.Instantiate();
            itemElement.Q<Label>("ItemName").text = item.itemName;
            itemElement.Q<Label>("ItemDescription").text = item.stats.ToString();
            //itemElement.Q<VisualElement>("ItemImage").style.backgroundImage = SpriteToTexture2D(item.sprite);
            root.Add(itemElement);
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