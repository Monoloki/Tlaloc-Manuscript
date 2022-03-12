using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "ArrayOfItems", menuName = "Items/ArrayOfAllItems")]
public class ArrayOfAllItems : ScriptableObject {

    [ReadOnly, SerializeField]
    private int lastUsedID;

    public Item[] items;
    
    private void Awake() {
        items = new Item[2000];
    }
    public int GetNewID() {
        for (int i = 0; i < items.Length; i++) {
            if (items[i] == null) {
                lastUsedID = i;
                break;
            }
        }
        return lastUsedID;
    }
}
