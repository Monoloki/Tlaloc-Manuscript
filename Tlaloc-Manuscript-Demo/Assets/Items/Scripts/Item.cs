using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    Tool,
    Weapon,
    Trash,
    Gold,
    Material,
    Default,
    Book
}

public abstract class Item : ScriptableObject {
    public GameObject prefabOfItem;
    public GameObject prefabToHoldInHand;
    public ItemType type;
    public Sprite sprite;
    public string itemName;
    public int iD;
    [TextArea(15, 20)]
    public string description;

    public virtual void Awake() {
        //var itemArray = Resources.Load<ArrayOfAllItems>("Arrays/ItemArray");
        //iD = itemArray.GetNewID();
        //itemArray.items[iD] = this;
    }

    private void OnValidate() {
        itemName = name;
    }
}
