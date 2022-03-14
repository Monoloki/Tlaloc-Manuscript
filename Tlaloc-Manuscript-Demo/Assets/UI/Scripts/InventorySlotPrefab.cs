using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotPrefab : MonoBehaviour
{
    public Button button;
    public TMP_Text ItemLabel;
    public ItemDescriptionWindowController itemDescriptionWindowController;
    public Item itemRef;

    public void ShowDescription() {
        itemDescriptionWindowController.itemDescriptionWindow.SetActive(true);
        itemDescriptionWindowController.itemNameLabel.text = itemRef.itemName;
        itemDescriptionWindowController.activeItem = itemRef;
    }
}
