using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class BuildingUiController : MonoBehaviour
{

    [Header("Building Menu")]

    [SerializeField] private GameObject buildingMenu;
    [SerializeField] private Image buildingImage;
    [SerializeField] private TMP_Text buildingField;
    [SerializeField] private TMP_Text requirementsField;
    [SerializeField] private BuildingSystem buildingSystem;

    private int activePNG = 0;

    private void Awake() {
        UpdateUi();
        buildingSystem.showBuildingPreview = true;
    }

    public void ActiveBuildingWindow() {
        buildingMenu.SetActive(!buildingMenu.activeSelf);
        buildingSystem.showBuildingPreview = buildingMenu.activeSelf;
    }  

    public void OnClickCancelButton() {
        DestroyBuildingPreview();
        buildingSystem.showBuildingPreview = false;
    }

    public void OnClickChooseButton() {
        buildingSystem.objectToInstantiate = buildingSystem.buildingArray[activePNG].prefab;
        buildingSystem.activeBuilding = buildingSystem.buildingArray[activePNG];
        buildingSystem.showBuildingPreview = true;
        buildingSystem.SpawnBuildingPreview();
    }

    public void OnClickNextButton() {
        DestroyBuildingPreview();
        activePNG++;
        if (activePNG >= buildingSystem.buildingArray.Length) {
            activePNG = 0;
        }

        UpdateUi();
    }

    public void OnClickPreviousButton() {
        DestroyBuildingPreview();
        activePNG--;
        if (activePNG < 0) {
            activePNG = buildingSystem.buildingArray.Length - 1;
        }

        UpdateUi();
    }

    private void UpdateUi() {
        UpdatePNG();
        UpdateText();
    }

    private void UpdatePNG() {
        buildingImage.sprite = buildingSystem.buildingArray[activePNG].sprite;
    }

    private void UpdateText() {
        buildingField.text = buildingSystem.buildingArray[activePNG].sprite.name;
        requirementsField.text = "";
        foreach (var material in buildingSystem.buildingArray[activePNG].requirements) {
            requirementsField.text += $"{material.material.name} {material.requireAmount}";
        }
    }

    private void DestroyBuildingPreview() {
        if (buildingSystem.instantiatedBuilding != null) {
            Destroy(buildingSystem.instantiatedBuilding);
            buildingSystem.isObjectInstantiated = false;
            buildingSystem.showBuildingPreview = false;
        }   
    }
}
