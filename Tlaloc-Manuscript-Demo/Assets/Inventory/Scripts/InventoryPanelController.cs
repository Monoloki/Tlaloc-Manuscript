using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelController : Singleton<InventoryPanelController>
{
    private void Awake() {
        UpdateInventoryUI();
    }

    public RectTransform leftHandWeaponSlot;
    public RectTransform rightHandWeaponSlot;
    public RectTransform leftHandToolSlot;
    public RectTransform rightHandToolSlot;
    public RectTransform bookSlot;

    [SerializeField] private EquipmentController equipmentController;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private UIInventoryController uIInventoryController;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private EquipedStatsModel equipedStatsModel;

    private InventorySlotPrefab leftWeaponSlotPrefab;
    private InventorySlotPrefab rightWeaponSlotPrefab;
    private InventorySlotPrefab leftToolSlotPrefab;
    private InventorySlotPrefab rightToolSlotPrefab;
    private InventorySlotPrefab bookSlotPrefab;

    public void EquipBook(Item item) {
        if (bookSlotPrefab == null) bookSlotPrefab = Instantiate(slotPrefab, bookSlot).GetComponent<InventorySlotPrefab>();
        else if (bookSlotPrefab != null) SwapItemUI(bookSlotPrefab);

        UpdateItemUIWithDestroy(item);

        bookSlotPrefab.itemRef = FindItemInInventory(item).item;
        bookSlotPrefab.ItemLabel.text = item.name;

        equipmentController.EquipItem(EquipmentSlot.book, bookSlotPrefab.itemRef.prefabToHoldInHand, bookSlotPrefab.itemRef);

        equipedStatsModel.UpdateEquipedStats();
    }
    public void EquipLeftHandWeapon(Item item) {
        if (leftWeaponSlotPrefab == null) leftWeaponSlotPrefab = Instantiate(slotPrefab, leftHandWeaponSlot).GetComponent<InventorySlotPrefab>();
        else if (leftWeaponSlotPrefab != null) SwapItemUI(leftWeaponSlotPrefab);

        UpdateItemUIWithDestroy(item);

        leftWeaponSlotPrefab.itemRef = FindItemInInventory(item).item;
        leftWeaponSlotPrefab.ItemLabel.text = item.name;

        equipmentController.EquipItem(EquipmentSlot.leftWeapon,leftWeaponSlotPrefab.itemRef.prefabToHoldInHand,leftWeaponSlotPrefab.itemRef);

        equipedStatsModel.UpdateEquipedStats();
    }
    public void EquipRightHandWeapon(Item item) {
        if (rightWeaponSlotPrefab == null) rightWeaponSlotPrefab = Instantiate(slotPrefab, rightHandWeaponSlot).GetComponent<InventorySlotPrefab>();
        else if (rightWeaponSlotPrefab != null) SwapItemUI(rightWeaponSlotPrefab);

        UpdateItemUIWithDestroy(item);

        rightWeaponSlotPrefab.itemRef = FindItemInInventory(item).item;
        rightWeaponSlotPrefab.ItemLabel.text = item.name;

        equipmentController.EquipItem(EquipmentSlot.rightWeapon, rightWeaponSlotPrefab.itemRef.prefabToHoldInHand, rightWeaponSlotPrefab.itemRef);

        equipedStatsModel.UpdateEquipedStats();
    }
    public void EquipLeftHandTool(Item item) {
        if (leftToolSlotPrefab == null) leftToolSlotPrefab = Instantiate(slotPrefab, leftHandToolSlot).GetComponent<InventorySlotPrefab>(); 
        else if (leftToolSlotPrefab != null) SwapItemUI(leftToolSlotPrefab);

        UpdateItemUIWithDestroy(item);

        leftToolSlotPrefab.itemRef = FindItemInInventory(item).item;
        leftToolSlotPrefab.ItemLabel.text = item.name;

        equipmentController.EquipItem(EquipmentSlot.leftTool, leftToolSlotPrefab.itemRef.prefabToHoldInHand);

        equipedStatsModel.UpdateEquipedStats();
    }
    public void EquipRightHandTool(Item Item) {
        if (rightToolSlotPrefab == null) rightToolSlotPrefab = Instantiate(slotPrefab, rightHandToolSlot).GetComponent<InventorySlotPrefab>();
        else if (rightToolSlotPrefab != null) SwapItemUI(rightToolSlotPrefab);

        UpdateItemUIWithDestroy(Item);

        rightToolSlotPrefab.itemRef = FindItemInInventory(Item).item;
        rightToolSlotPrefab.ItemLabel.text = Item.name;

        equipmentController.EquipItem(EquipmentSlot.rightTool, rightToolSlotPrefab.itemRef.prefabToHoldInHand);

        equipedStatsModel.UpdateEquipedStats();
    }
    private void SwapItemUI(InventorySlotPrefab inventorySlotPrefab) {
        var item =  FindItemInInventory(inventorySlotPrefab.itemRef);

        if (item.amount == 1) {
            uIInventoryController.CreateUiSlot(item.item,0);
        }
        else if (item.amount > 1) {
            uIInventoryController.UpdateUILabel(item.item);
        }
         
    }
    public InventorySlot FindItemInInventory(Item item) {

        foreach (var inventorySlot in playerInventory.container) {
            if (inventorySlot.item.iD == item.iD ) {
                return inventorySlot;
            }
        }

        Debug.Log($"I dont find item of name:{item} in playerInventory");
        return null;
    }

    private void UpdateItemUIWithDestroy(Item item) {
        var foundItem = FindItemInInventory(item);
        item = foundItem.item;
        var inventorySlot = uIInventoryController.activeItemsSlots[item];

        if (foundItem.amount > 1) {
            uIInventoryController.UpdateUILabel(item);
        }
        else if (foundItem.amount <= 1) {
            Destroy(inventorySlot.gameObject);
        }
    }

    private void UpdateItemUI(Item item) {
        var foundItem = FindItemInInventory(item);
        item = foundItem.item;
        var inventorySlot = uIInventoryController.activeItemsSlots[item];

        if (foundItem.amount >= 1) {
            uIInventoryController.UpdateUILabel(item);
        }
        else if (foundItem.amount < 1) {
            Destroy(inventorySlot.gameObject);
        }
    }

    public void UpdateInventoryUI() {
        foreach (var slot in playerInventory.container) {
            UpdateItemUI(slot.item);
        }
    }


}


//stworzyc ikone
//przypisac wartosci
//zespawnowac object w rece