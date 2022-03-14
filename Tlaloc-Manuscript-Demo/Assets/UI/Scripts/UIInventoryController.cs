using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryController : MonoBehaviour {

    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private GridLayoutGroup gridLayout;
    [SerializeField] private ItemDescriptionWindowController itemDescriptionWindowController;

    public Dictionary<Item, InventorySlotPrefab> activeItemsSlots = new Dictionary<Item, InventorySlotPrefab>();

    private bool isInvoking = false;

    private void Awake() {
        gridLayout.enabled = false;
    }

    public void AddItemToInventoryUI(Item _item, int _amount) {

        gridLayout.enabled = true;

        if (!activeItemsSlots.ContainsKey(_item)) {
            var slot = Instantiate(itemSlotPrefab,transform).GetComponent<InventorySlotPrefab>();
            slot.itemRef = _item;
            slot.itemDescriptionWindowController = this.itemDescriptionWindowController;
            activeItemsSlots.Add(_item, slot);
        }

        UpdateAmount(_item, _amount);

        if (!isInvoking) {
            StartCoroutine(TurnOffGridLayerGroup());
        }

    }

    public void RefreshInventoryUI() {
        foreach (var inventorySlot in playerInventory.container) {
            
        }
    }

    private void UpdateAmount(Item _item, int _newAmount) {
        if (_newAmount > 1) {
            activeItemsSlots[_item].ItemLabel.text = $"{activeItemsSlots[_item].name} \n {_newAmount}";
        }
        else {
            activeItemsSlots[_item].ItemLabel.text = $"{_item.itemName}";
        }
    }

    private IEnumerator TurnOffGridLayerGroup() {
        isInvoking = true;
        yield return new WaitForEndOfFrame();
        gridLayout.enabled = false;
        isInvoking = false;
    }

}
