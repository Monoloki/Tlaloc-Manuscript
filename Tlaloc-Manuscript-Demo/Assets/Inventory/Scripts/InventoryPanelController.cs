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

    [SerializeField] private Inventory playerInventory;

    [SerializeField] private UIInventoryController uIInventoryController;

    [SerializeField] private GameObject slotPrefab;

    private InventorySlotPrefab leftWeaponSlotPrefab;
    private InventorySlotPrefab rightWeaponSlotPrefab;
    private InventorySlotPrefab leftToolSlotPrefab;
    private InventorySlotPrefab rightToolSlotPrefab;
    private InventorySlotPrefab bookSlotPrefab;

    public void EquipBook(string itemName) {
        if (bookSlotPrefab == null) bookSlotPrefab = Instantiate(slotPrefab, bookSlot).GetComponent<InventorySlotPrefab>();

        bookSlotPrefab.ItemLabel.text = itemName;

    }
    public void EquipLeftHandWeapon(string itemName) {
        if (leftWeaponSlotPrefab == null) leftWeaponSlotPrefab = Instantiate(slotPrefab, leftHandWeaponSlot).GetComponent<InventorySlotPrefab>();

        //Destroy(uIInventoryController.activeItemsSlots[FindItemInInventoryByName(itemName)].gameObject); dodac zale¿noœæ od iloœci

        leftWeaponSlotPrefab.ItemLabel.text = itemName;

    }
    public void EquipRightHandWeapon(string itemName) {
        if (rightWeaponSlotPrefab == null) rightWeaponSlotPrefab = Instantiate(slotPrefab, rightHandWeaponSlot).GetComponent<InventorySlotPrefab>();

        rightWeaponSlotPrefab.ItemLabel.text = itemName; 

    }
    public void EquipLeftHandTool(string itemName) {
        if (leftToolSlotPrefab == null) leftToolSlotPrefab = Instantiate(slotPrefab, leftHandToolSlot).GetComponent<InventorySlotPrefab>();

        leftToolSlotPrefab.ItemLabel.text = itemName; 
 
    }
    public void EquipRightHandTool(string itemName) {
        if (rightToolSlotPrefab == null) rightToolSlotPrefab = Instantiate(slotPrefab, rightHandToolSlot).GetComponent<InventorySlotPrefab>();

        rightToolSlotPrefab.ItemLabel.text = itemName; 
        
    }

    private Item FindItemInInventoryByName(string itemName) {

        foreach (var inventorySlot in playerInventory.container) {
            if (inventorySlot.item.itemName == itemName ) {
                return inventorySlot.item;
            }
        }

        Debug.Log($"I dont find item of name:{itemName} in playerInventory");
        return null;
       
    }

}


//stworzyc ikone
//przypisac wartosci
//zespawnowac object w rece