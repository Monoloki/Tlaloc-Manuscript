using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Containter", menuName = "Towers/Tower Containter")]
public class TowerInventory : ScriptableObject
{
    public List<TowerSlot> container = new List<TowerSlot>();

    public void AddItem(Tower tower, int amount) {
        bool hasItem = false;
        for (int i = 0; i < container.Count; i++) {
            if (container[i].tower == tower) {
                container[i].AddAmount(amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem) {
            container.Add(new TowerSlot(tower, amount));
        }
    }
}

[System.Serializable]
public class TowerSlot {
    public Tower tower;
    public int amount;

    public TowerSlot(Tower tower, int amount) {
        this.tower = tower;
        this.amount = amount;
    }

    public void AddAmount(int value) {
        amount += value;
    }
}
