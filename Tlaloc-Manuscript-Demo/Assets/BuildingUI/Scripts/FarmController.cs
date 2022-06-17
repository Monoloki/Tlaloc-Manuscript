using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmController : MonoBehaviour
{
    public TowerSegmentView segmentPrefab;
    public VerticalLayoutGroup layoutGroup;
    public Tower[] towersToBuy;

    private void Start() {
        layoutGroup.enabled = false;
        SpawnAllSegments();
    }

    private void SpawnAllSegments() {
        foreach (var tower in towersToBuy) {
            SpawnSegment(tower);
        }
        StartCoroutine(VerticalLayoutReset());
    }

    private void AddSegment(Tower tower) {
        SpawnSegment(tower);
        StartCoroutine(VerticalLayoutReset());
    }

    private void SpawnSegment(Tower tower) {
        TowerSegmentView spawnedSegment = Instantiate(segmentPrefab, layoutGroup.transform);
        spawnedSegment.Name.text = $"Name: {tower.name}";
        spawnedSegment.Price.text = $"Price: {tower.price}";
        spawnedSegment.buyButton.onClick.AddListener(() => BuyTower(tower));
    }

    private IEnumerator VerticalLayoutReset() {
        layoutGroup.enabled = true;
        yield return new WaitForEndOfFrame();
        layoutGroup.enabled = false;
    }

    private void BuyTower(Tower tower) {

        var slot = PlayerManager.instance.playerInventory.container.Find(inventoryslot => inventoryslot.item.type == ItemType.Gold);

        if (tower.price <= slot.amount) {
            PlayerManager.instance.playerInventory.RemoveItem(slot.item, tower.price);
            PlayerManager.instance.towerInventory.AddItem(tower, 1);
        }

        FindObjectOfType<InventoryPanelController>().UpdateInventoryUI();

        //InventoryPanelController.instance.UpdateInventoryUI();
        Debug.Log("Succes buy");


        try {
            

        }
        catch (NullReferenceException ex) {
            Debug.Log("Not enough gold");

        }
    }

}
