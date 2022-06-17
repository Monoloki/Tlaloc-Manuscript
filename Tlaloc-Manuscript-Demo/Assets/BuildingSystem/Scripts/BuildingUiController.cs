using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class BuildingUiController : MonoBehaviour { 

    [Header("Building Menu")]

    [SerializeField] private GameObject buildingMenu;
    [SerializeField] private Image buildingImage;
    [SerializeField] private TMP_Text buildingField;
    [SerializeField] private TMP_Text requirementsField;
    [SerializeField] private BuildingSystem buildingSystem;

    public bool towerBuildingMode = false;

    private int active = 0;

    private void Awake() {
        UpdateUi();
        buildingSystem.showBuildingPreview = true;
    }

    public void ActiveBuildingWindow() {
        buildingMenu.SetActive(!buildingMenu.activeSelf);
        if (!buildingMenu.activeSelf) {
            buildingSystem.showBuildingPreview = false;
        }
    }

    public void ResetActive() {
        active = 0;
        UpdateUi();
    }

    public void OnClickCancelButton() {
        DestroyBuildingPreview();
    }

    public void OnClickChooseButton() {

        if (!towerBuildingMode) {
            buildingSystem.activeBuilding = buildingSystem.buildingArray[active];
        }
        else {
            buildingSystem.activeTower = buildingSystem.towerArray[active];
        }
            
        buildingSystem.showBuildingPreview = true;
        buildingSystem.SpawnBuildingPreview();
    }

    public void OnClickNextButton() {
        DestroyBuildingPreview();
        active++;
        if (!towerBuildingMode) {
            if (active >= buildingSystem.buildingArray.Length) {
                active = 0;
            }
        }
        else {
            if (active >= buildingSystem.towerArray.Length) {
                active = 0;
            }
        }           

        UpdateUi();
    }

    public void OnClickPreviousButton() {
        DestroyBuildingPreview();
        active--;
        if (active < 0) {
            if (!towerBuildingMode) {
                active = buildingSystem.buildingArray.Length - 1;
            }
            else {
                active = buildingSystem.towerArray.Length - 1;
            }              
        }

        UpdateUi();
    }

    public void UpdateUi() {
        UpdatePNG();
        UpdateText();
    }

    private void UpdatePNG() {
        if (!towerBuildingMode) {
            buildingImage.sprite = buildingSystem.buildingArray[active].sprite;
        }
        else {
            buildingImage.sprite = buildingSystem.towerArray[active].icon;
        }
        
    }

    private void UpdateText() {
        if (!towerBuildingMode) {
            buildingField.text = buildingSystem.buildingArray[active].sprite.name;
            requirementsField.text = "";
            foreach (var material in buildingSystem.buildingArray[active].requirements) {
            requirementsField.text += $"{material.material.name}:{material.requireAmount} ";
            }
        }
        else {
            buildingField.text = buildingSystem.towerArray[active].name;
            requirementsField.text = $"Amount:{buildingSystem.playerManager.towerInventory.container.Find(x => x.tower.name == buildingField.text).amount - buildingSystem.GetAmountOfSpawnedTower(buildingSystem.towerArray[active])}";
        }
    }

    private void DestroyBuildingPreview() {
        if (buildingSystem.instantiatedModel != null) {
            Destroy(buildingSystem.instantiatedModel);
            //Debug.Log("zniszczony preview");
            buildingSystem.isTowerInstantiated = false;
            buildingSystem.isObjectInstantiated = false;
            buildingSystem.showBuildingPreview = false;
        }   
    }
}
