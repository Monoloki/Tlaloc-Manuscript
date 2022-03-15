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

    [HideInInspector] public Item activeItem;

    public void OnEquipLeftButtonClick() {
        switch (activeItem.type) {
            case ItemType.Tool:
                inventoryPanelController.EquipLeftHandTool(activeItem.itemName);
                break;
            case ItemType.Weapon:
                inventoryPanelController.EquipLeftHandWeapon(activeItem.itemName);
                break;              
            case ItemType.Book:
                inventoryPanelController.EquipBook(activeItem.itemName);
                break;
            default:
                break;
        }

        OnCloseButtonClick();
    }

    public void OnEquipRightButtonClick() {
        switch (activeItem.type) {
            case ItemType.Tool:
                inventoryPanelController.EquipRightHandTool(activeItem.itemName);
                break;
            case ItemType.Weapon:
                inventoryPanelController.EquipRightHandWeapon(activeItem.itemName);
                break;
            case ItemType.Book:
                inventoryPanelController.EquipBook(activeItem.itemName);
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


    public void UpdateInventoryIcon() {
       

    
    }
}
