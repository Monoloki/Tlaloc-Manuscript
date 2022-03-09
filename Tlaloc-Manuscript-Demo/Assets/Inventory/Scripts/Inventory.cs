using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField]
    private ItemIntDictionary container = new ItemIntDictionary();

    private int basicSlots = 20;

    public void AddItem(Item itemToAdd,int amount) {
        foreach (var item in container) {
            if (item.Key.iD == itemToAdd.iD) {
                container[itemToAdd]++;
                break;
            }
            else {
                container.Add(itemToAdd,amount);
            }
        }
    }

    public void AddItem(Item itemToAdd) { 
    
    }
}




[System.Serializable]
public class ItemIntDictionary : SerializableDictionary<Item, int> { }