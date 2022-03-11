using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Inventory playerInventory;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.TryGetComponent<ItemObject>(out ItemObject _itemObject)) {
            playerInventory.AddItem(_itemObject.item, _itemObject.amount);
            Destroy(other.gameObject);
        }
    }
}
