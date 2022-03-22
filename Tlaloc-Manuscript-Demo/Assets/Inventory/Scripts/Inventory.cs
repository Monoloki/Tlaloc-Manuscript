using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "InventorySysytem/Inventory")]
public class Inventory : ScriptableObject
{
    public List<InventorySlot> container = new List<InventorySlot>();

    public void AddItem(Item _item,int _amount) {
        bool hasItem = false;
        for (int i = 0; i < container.Count; i++) {
            if (container[i].item.iD == _item.iD) {
                container[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem) {
            container.Add(new InventorySlot(_item, _amount));
        }
    }

    // Method to remove items with UI removing
    public void RemoveItem(string itemName, int amount) {

        for (int i = 0; i < container.Count; i++) {
            
            if (container[i].item.itemName == itemName) {

                if (container[i].amount <= amount) {
                    container[i] = null;


                    Debug.Log($"Iitem: {itemName} has been removed");
                }
                else if (container[i].amount > amount ) {
                    var newAmount = container[i].amount -= amount;

                    Debug.Log($"Iitem: {itemName}, in amount of: {amount} has been removed, amount left {newAmount}");
                }
                break;
            }          
        }
    }

}

[System.Serializable]
public class InventorySlot {
    public Item item;
    public int amount;
    public InventorySlot(Item _item,int _amount) {
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value) {
        amount += value;
    }
}
