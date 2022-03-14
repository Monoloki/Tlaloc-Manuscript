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

    [SerializeField] private GameObject slotPrefab;

    private InventorySlotPrefab leftWeaponSlotPrefab;
    private InventorySlotPrefab rightWeaponSlotPrefab;
    private InventorySlotPrefab leftToolSlotPrefab;
    private InventorySlotPrefab rightToolSlotPrefab;
    private InventorySlotPrefab bookSlotPrefab;

    public void EquipBook(string itemName) {
        if (bookSlotPrefab == null) bookSlotPrefab = Instantiate(slotPrefab, bookSlot).GetComponent<InventorySlotPrefab>();
        //bookSlotPrefab.transform.position = bookSlot.t;
        bookSlotPrefab.ItemLabel.text = itemName;

    }
    public void EquipLeftHandWeapon(string itemName) {
        if (leftWeaponSlotPrefab == null) leftWeaponSlotPrefab = Instantiate(slotPrefab, leftHandWeaponSlot).GetComponent<InventorySlotPrefab>();
        //leftWeaponSlotPrefab.transform.position = leftHandWeaponSlot.anchoredPosition;
        leftWeaponSlotPrefab.ItemLabel.text = itemName;

    }
    public void EquipRightHandWeapon(string itemName) {
        if (rightWeaponSlotPrefab == null) rightWeaponSlotPrefab = Instantiate(slotPrefab, rightHandWeaponSlot).GetComponent<InventorySlotPrefab>();
        //rightWeaponSlotPrefab.transform.position = rightHandWeaponSlot.anchoredPosition;
        rightWeaponSlotPrefab.ItemLabel.text = itemName; 

    }
    public void EquipLeftHandTool(string itemName) {
        if (leftToolSlotPrefab == null) leftToolSlotPrefab = Instantiate(slotPrefab, leftHandToolSlot).GetComponent<InventorySlotPrefab>();
        //leftToolSlotPrefab.transform.position = leftHandToolSlot.anchoredPosition;
        leftToolSlotPrefab.ItemLabel.text = itemName; 
 
    }
    public void EquipRightHandTool(string itemName) {
        if (rightToolSlotPrefab == null) rightToolSlotPrefab = Instantiate(slotPrefab, rightHandToolSlot).GetComponent<InventorySlotPrefab>();
        //rightToolSlotPrefab.transform.position = rightHandToolSlot.anchoredPosition;
        rightToolSlotPrefab.ItemLabel.text = itemName; 
        
    }

}


//stworzyc ikone
//przypisac wartosci
//zespawnowac object w rece