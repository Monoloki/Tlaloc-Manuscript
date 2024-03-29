using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryController : MonoBehaviour {

    [SerializeField] private InventoryPanelController inventoryPanelController;
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private GridLayoutGroup gridLayout;
    [SerializeField] private ItemDescriptionWindowController itemDescriptionWindowController;

    public Dictionary<Item, InventorySlotPrefab> activeItemsSlots = new Dictionary<Item, InventorySlotPrefab>();

    private bool isInvoking = false;

    private void Awake() {
        gridLayout.enabled = false;
    }

    public void CreateUiSlot(Item _item, int _amount) {
        gridLayout.enabled = true;

        var slot = Instantiate(itemSlotPrefab, transform).GetComponent<InventorySlotPrefab>();
        slot.itemRef = _item;
        slot.itemDescriptionWindowController = this.itemDescriptionWindowController;

        if (activeItemsSlots[_item] == null) {
            activeItemsSlots[_item] = slot;
        }         

        AddAmountLabel(_item, 0);

        if (!isInvoking) {
            StartCoroutine(TurnOffGridLayerGroup());
        }
    }

    public void AddItemToInventoryUI(Item _item, int _amount) {

        gridLayout.enabled = true;

        if (!activeItemsSlots.ContainsKey(_item)) {
            var slot = Instantiate(itemSlotPrefab,transform).GetComponent<InventorySlotPrefab>();
            slot.itemRef = _item;
            slot.itemDescriptionWindowController = this.itemDescriptionWindowController;
            activeItemsSlots.Add(_item, slot);
        }

        AddAmountLabel(_item, 0);

        if (!isInvoking) {
            StartCoroutine(TurnOffGridLayerGroup());
        }

    }

    // Remove UI method
    public void removeUISlot(Item _item) {

        Destroy(activeItemsSlots[_item].gameObject);

        if (!isInvoking) {
            StartCoroutine(TurnOffGridLayerGroup());
        }
    }

    public void AddAmountLabel(Item _item, int _amountToAdd) {
        var e = inventoryPanelController.FindItemInInventoryByName(_item.itemName).amount;
        activeItemsSlots[_item].ItemLabel.text = $"{_item.itemName} \n {e + _amountToAdd}";
    }

    private IEnumerator TurnOffGridLayerGroup() {
        isInvoking = true;
        yield return new WaitForEndOfFrame();
        gridLayout.enabled = false;
        isInvoking = false;
    }

}
