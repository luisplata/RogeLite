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