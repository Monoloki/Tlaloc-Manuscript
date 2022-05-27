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
    [SerializeField] private Sprite[] buildingPNG;
    [SerializeField] private TMP_Text buildingName;
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
        buildingSystem.objectToInstantiate = buildingSystem.buildingArray[activePNG];
        buildingSystem.showBuildingPreview = true;
        buildingSystem.SpawnBuildingPreview();
    }

    public void OnClickNextButton() {
        DestroyBuildingPreview();
        activePNG++;
        if (activePNG >= buildingPNG.Length) {
            activePNG = 0;
        }

        UpdateUi();
    }

    public void OnClickPreviousButton() {
        DestroyBuildingPreview();
        activePNG--;
        if (activePNG < 0) {
            activePNG = buildingPNG.Length - 1;
        }

        UpdateUi();
    }

    private void UpdateUi() {
        UpdatePNG();
        UpdateText();
    }

    private void UpdatePNG() {
        buildingImage.sprite = buildingPNG[activePNG];
    }

    private void UpdateText() {
        buildingName.text = buildingPNG[activePNG].name;
    }

    private void DestroyBuildingPreview() {
        if (buildingSystem.instantiatedBuilding != null) {
            Destroy(buildingSystem.instantiatedBuilding);
            buildingSystem.isObjectInstantiated = false;
            buildingSystem.showBuildingPreview = false;
        }   
    }
}
