using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelController : MonoBehaviour
{
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

    public void EquipBook(string itemName) {
        if (bookSlotPrefab == null) bookSlotPrefab = Instantiate(slotPrefab, bookSlot).GetComponent<InventorySlotPrefab>();
        else if (bookSlotPrefab != null) SwapItemUI(bookSlotPrefab);

        UpdateInventoryUI(itemName);

        bookSlotPrefab.itemRef = FindItemInInventoryByName(itemName).item;
        bookSlotPrefab.ItemLabel.text = itemName;

        equipmentController.EquipItem(EquipmentSlot.book, bookSlotPrefab.itemRef.prefabToHoldInHand, bookSlotPrefab.itemRef);

        equipedStatsModel.UpdateEquipedStats();
    }
    public void EquipLeftHandWeapon(string itemName) {
        if (leftWeaponSlotPrefab == null) leftWeaponSlotPrefab = Instantiate(slotPrefab, leftHandWeaponSlot).GetComponent<InventorySlotPrefab>();
        else if (leftWeaponSlotPrefab != null) SwapItemUI(leftWeaponSlotPrefab);

        UpdateInventoryUI(itemName);        

        leftWeaponSlotPrefab.itemRef = FindItemInInventoryByName(itemName).item;
        leftWeaponSlotPrefab.ItemLabel.text = itemName;

        equipmentController.EquipItem(EquipmentSlot.leftWeapon,leftWeaponSlotPrefab.itemRef.prefabToHoldInHand,leftWeaponSlotPrefab.itemRef);

        equipedStatsModel.UpdateEquipedStats();
    }
    public void EquipRightHandWeapon(string itemName) {
        if (rightWeaponSlotPrefab == null) rightWeaponSlotPrefab = Instantiate(slotPrefab, rightHandWeaponSlot).GetComponent<InventorySlotPrefab>();
        else if (rightWeaponSlotPrefab != null) SwapItemUI(rightWeaponSlotPrefab);

        UpdateInventoryUI(itemName);

        rightWeaponSlotPrefab.itemRef = FindItemInInventoryByName(itemName).item;
        rightWeaponSlotPrefab.ItemLabel.text = itemName;

        equipmentController.EquipItem(EquipmentSlot.rightWeapon, rightWeaponSlotPrefab.itemRef.prefabToHoldInHand, rightWeaponSlotPrefab.itemRef);

        equipedStatsModel.UpdateEquipedStats();
    }

    public void EquipLeftHandTool(string itemName) {
        if (leftToolSlotPrefab == null) leftToolSlotPrefab = Instantiate(slotPrefab, leftHandToolSlot).GetComponent<InventorySlotPrefab>(); 
        else if (leftToolSlotPrefab != null) SwapItemUI(leftToolSlotPrefab);

        UpdateInventoryUI(itemName);

        leftToolSlotPrefab.itemRef = FindItemInInventoryByName(itemName).item;
        leftToolSlotPrefab.ItemLabel.text = itemName;

        equipmentController.EquipItem(EquipmentSlot.leftTool, leftToolSlotPrefab.itemRef.prefabToHoldInHand);

        equipedStatsModel.UpdateEquipedStats();
    }
    public void EquipRightHandTool(string itemName) {
        if (rightToolSlotPrefab == null) rightToolSlotPrefab = Instantiate(slotPrefab, rightHandToolSlot).GetComponent<InventorySlotPrefab>();
        else if (rightToolSlotPrefab != null) SwapItemUI(rightToolSlotPrefab);

        UpdateInventoryUI(itemName);

        rightToolSlotPrefab.itemRef = FindItemInInventoryByName(itemName).item;
        rightToolSlotPrefab.ItemLabel.text = itemName;

        equipmentController.EquipItem(EquipmentSlot.rightTool, rightToolSlotPrefab.itemRef.prefabToHoldInHand);

        equipedStatsModel.UpdateEquipedStats();
    }

    private void SwapItemUI(InventorySlotPrefab inventorySlotPrefab) {
        var aa =  FindItemInInventoryByName(inventorySlotPrefab.itemRef.itemName);

        if (aa.amount == 1) {
            uIInventoryController.CreateUiSlot(aa.item,0);
        }
        else if (aa.amount > 1) {
            uIInventoryController.AddAmountLabel(aa.item,0);
        }
         
    }

    public InventorySlot FindItemInInventoryByName(string itemName) {

        foreach (var inventorySlot in playerInventory.container) {
            if (inventorySlot.item.itemName == itemName ) {
                return inventorySlot;
            }
        }

        Debug.Log($"I dont find item of name:{itemName} in playerInventory");
        return null;
       
    }

    private void UpdateInventoryUI(string _itemName) {
        var aa = FindItemInInventoryByName(_itemName);

        var a = uIInventoryController.activeItemsSlots[aa.item];

        if (aa.amount > 1) {
            uIInventoryController.AddAmountLabel(aa.item, -1);
        }
        else if (aa.amount <= 1) {
            Destroy(a.gameObject);
        }
    }


}


//stworzyc ikone
//przypisac wartosci
//zespawnowac object w rece