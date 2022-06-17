using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField] private LevelController levelController;
    [SerializeField] private BuildingUiController buildingUiController;
    [SerializeField] private BuildingSystem buildingSystem;


    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Player") {
            levelController.LoadLevelAsync();

            BuildingSystemSetup();
        }
    }

    private void BuildingSystemSetup() {
        buildingSystem.towerBuildingMode = true;
        buildingUiController.towerBuildingMode = true;
        buildingUiController.ResetActive();
    }
}
