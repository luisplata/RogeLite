using Items.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    public class ItemInstanceUiModal : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI itemDescription;
        [SerializeField] private Button equipButton, detailsButton;
        [SerializeField] private GameObject detailRoot;
        [SerializeField] private GameObject starPrefab;
        [SerializeField] private GameObject starParent;

        public void Initialize(LootItemInstance item, EquipmentCanvasUiModal equipmentCanvasUiModal)
        {
            itemName.text = item.Data.itemName;
            itemDescription.text = item.Data.baseStats.ToString();
            itemImage.sprite = item.Data.itemSprite;
            for (int i = 0; i < item.Stars; i++)
            {
                Instantiate(starPrefab, starParent.transform);
            }
            equipButton.onClick.AddListener(() =>
            {
                equipmentCanvasUiModal.EquipItem(item);
            });
            detailRoot.SetActive(false);

            detailsButton.onClick.AddListener(() => { detailRoot.SetActive(!detailRoot.activeSelf); });
        }
    }
}