using UnityEngine;
using UnityEngine.UI;

public class EquipmentCanvasUiModal : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject contentItems;
    private EquipmentCanvasUi _canvas;

    public void Initialize(EquipmentCanvasUi canvas)
    {
        _canvas = canvas;
    }

    private void Start()
    {
        closeButton.onClick.AddListener(() => { _canvas.CloseModal(); });
    }

    public void Hide()
    {
        root.SetActive(false);
    }
}