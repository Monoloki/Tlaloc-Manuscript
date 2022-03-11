using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    Tool,
    Weapon,
    Trash,
    Gold,
    Material,
    Default
}

public abstract class Item : ScriptableObject {
    public GameObject prefab;
    public GameObject model;
    public ItemType type;
    public Sprite sprite;
    [ReadOnly]
    public int iD;
    [TextArea(15, 20)]
    public string description;

    public virtual void Awake() {
        var itemArray = Resources.Load<ArrayOfAllItems>("Arrays/ItemArray");
        iD = itemArray.GetNewID();
        itemArray.items[iD] = this;
    }

}
