using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDescriptionWindowController : MonoBehaviour
{
    [SerializeField] private InventoryPanelController inventoryPanelController;
    [SerializeField] private UIInventoryController uIInventoryController;

    public TMP_Text itemNameLabel;
    public GameObject itemDescriptionWindow;

    public Item activeItem;

    private void Awake() {
        gameObject.SetActive(false);
    }

    public void OnEquipLeftButtonClick() {
        switch (activeItem.type) {
            case ItemType.Tool:
                inventoryPanelController.EquipLeftHandTool(activeItem);
                break;
            case ItemType.Weapon:
                inventoryPanelController.EquipLeftHandWeapon(activeItem);
                break;              
            case ItemType.Book:
                inventoryPanelController.EquipBook(activeItem);
                break;
            default:
                break;
        }

        OnCloseButtonClick();
    }

    public void OnEquipRightButtonClick() {
        switch (activeItem.type) {
            case ItemType.Tool:
                inventoryPanelController.EquipRightHandTool(activeItem);
                break;
            case ItemType.Weapon:
                inventoryPanelController.EquipRightHandWeapon(activeItem);
                break;
            case ItemType.Book:
                inventoryPanelController.EquipBook(activeItem);
                break;
            default:
                break;
        }

        OnCloseButtonClick();
    }

    public void OnDropButtonClick() {

        //drop

        OnCloseButtonClick();
    }

    public void OnCloseButtonClick() {
        itemDescriptionWindow.SetActive(false);
        activeItem = null;
    }

    public void UpdateItemNameLabel(string itemName) {
        itemNameLabel.text = itemName;
    }

    /*
    public void UpdateInventoryIcon() {
       

    
    }
    */
}
