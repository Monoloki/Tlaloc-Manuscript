using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class BuildingSystem : MonoBehaviour {

    [SerializeField] private Transform origin;
    [SerializeField] private Transform targetDirection;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask towerLayerMask;
    [SerializeField] private float maxDistance;
    [SerializeField] private Material transparentMaterial;
    [SerializeField] private Material errorTransparentMaterial;
    [SerializeField] private BuildingUiController buildingUiController;
    public PlayerManager playerManager;

    private RaycastHit _hit;
    private Vector3 _direction;
    private Vector3 hitPositionValue;
    private bool isHitValue;

    [HideInInspector] public BuildingsStats activeBuilding;
    [HideInInspector] public Tower activeTower;

    public BuildingsStats[] buildingArray;
    public Tower[] towerArray;

    public List<GameObject> spawnedTowers = new List<GameObject>();

    public bool towerBuildingMode = false;  

    public bool isObjectInstantiated = false;
    public bool isTowerInstantiated = false;
    public bool showBuildingPreview = false;
    public InputActionReference spawnBuilding;
    public GameObject instantiatedModel;

    private void Awake() {
        spawnBuilding.action.performed += ToggleSpawnBuilding;     
    }

    private void Update(){ 
        if (showBuildingPreview) {
            GetRaycastHit();
            ShowBuilding();
        }
    }

    private void ToggleSpawnBuilding(InputAction.CallbackContext context) {       

        SpawnBuilding();

    }

    private void SpawnBuilding() {
        if (!towerBuildingMode && isObjectInstantiated) {
            if (BuildRequirementsCheck(activeBuilding)) {
                Instantiate(activeBuilding.prefab, instantiatedModel.transform.position, instantiatedModel.transform.rotation);
                Destroy(instantiatedModel);
                isObjectInstantiated = false;
                showBuildingPreview = false;
            }
            else {
                StopAllCoroutines();
                StartCoroutine(ErrorNotEnoughMats(instantiatedModel));
            }
        }
        else if (towerBuildingMode && isTowerInstantiated) {
            if (playerManager.towerInventory.container.Find(x => x.tower.name == activeTower.name).amount > GetAmountOfSpawnedTower(activeTower)) {
                spawnedTowers.Add(Instantiate(activeTower.Prefab, instantiatedModel.transform.position, instantiatedModel.transform.rotation));
                buildingUiController.UpdateUi();
                Destroy(instantiatedModel);
                isTowerInstantiated = false;
                showBuildingPreview = false;
            }
            else {
                StopAllCoroutines();
                StartCoroutine(ErrorNotEnoughMats(instantiatedModel));
            }
        }       
    }

    public int GetAmountOfSpawnedTower(Tower tower) {
        int amount = 0;
        foreach (var spawned in spawnedTowers) {          
            if (spawned.name == tower.Prefab.name +"(Clone)") {
                amount++;
            }
        }
        return amount;
    }

    public void SpawnBuildingPreview() {
        if (!isObjectInstantiated && !towerBuildingMode) {
            instantiatedModel = Instantiate(activeBuilding.prefab, hitPositionValue, activeBuilding.prefab.transform.rotation);
            
            foreach (MeshCollider meshCollider in instantiatedModel.GetComponentsInChildren<MeshCollider>()) {
                meshCollider.enabled = false;
            }

            foreach (Renderer renderer in instantiatedModel.GetComponentsInChildren<Renderer>()) {
                Material[] temporalMaterial = new Material[renderer.materials.Length];
                for (int i = 0; i < renderer.materials.Length; i++) {
                    temporalMaterial[i] = transparentMaterial;
                }
                renderer.materials = temporalMaterial;    
            }

            isObjectInstantiated = true;
        }
        else if (towerBuildingMode && !isTowerInstantiated) {
            instantiatedModel = Instantiate(activeTower.Prefab, hitPositionValue, activeTower.Prefab.transform.rotation);

            foreach (MeshCollider meshCollider in instantiatedModel.GetComponentsInChildren<MeshCollider>()) {
                meshCollider.enabled = false;
            }

            foreach (Renderer renderer in instantiatedModel.GetComponentsInChildren<Renderer>()) {
                Material[] temporalMaterial = new Material[renderer.materials.Length];
                for (int i = 0; i < renderer.materials.Length; i++) {
                    temporalMaterial[i] = transparentMaterial;
                }
                renderer.materials = temporalMaterial;
            }

            isTowerInstantiated = true;
        }
    }

    private void ShowBuilding(){
        if (isHitValue && isObjectInstantiated || isTowerInstantiated && isHitValue) {
            instantiatedModel.transform.position = hitPositionValue;
        }
    }

    private void GetRaycastHit(){
        if (!towerBuildingMode) {
            _direction = Vector3.Normalize(targetDirection.position - origin.position);
            if (Physics.Raycast(origin.position, _direction, out _hit, maxDistance, layerMask)) {
                Debug.DrawRay(origin.position, _direction, Color.blue);
                hitPositionValue = _hit.point;
                isHitValue = true;
            }
            else {
                Debug.DrawRay(origin.position, _direction, Color.yellow);
                isHitValue = false;
            }
        }
        else {
            _direction = Vector3.Normalize(targetDirection.position - origin.position);
            if (Physics.Raycast(origin.position, _direction, out _hit, maxDistance, towerLayerMask)) {
                Debug.DrawRay(origin.position, _direction, Color.blue);
                hitPositionValue = _hit.point;
                isHitValue = true;
            }
            else {
                Debug.DrawRay(origin.position, _direction, Color.yellow);
                isHitValue = false;
            }
        }

            

    }

    private bool BuildRequirementsCheck(BuildingsStats statsToCheck) {
        bool requireMet = false;
        foreach (var requirement in statsToCheck.requirements) {
            foreach (var item in playerManager.playerInventory.container) {
                if (item.item.iD == requirement.material.iD && item.amount >= requirement.requireAmount) {
                    playerManager.playerInventory.RemoveItem(requirement.material, requirement.requireAmount);
                    requireMet = true;
                    break;
                }
            }
            if (!requireMet) {
                return false;
            }
            requireMet = false;
        }
        return true;
    }

    private IEnumerator ErrorNotEnoughMats(GameObject previewObject) {
        var renderers = previewObject.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers) {
            Material[] temporalMaterial = new Material[renderer.materials.Length];
            for (int i = 0; i < renderer.materials.Length; i++) {
                temporalMaterial[i] = errorTransparentMaterial;
            }
            renderer.materials = temporalMaterial;
        }
        yield return new WaitForSeconds(5);

        foreach (Renderer renderer in renderers) {
            Material[] temporalMaterial = new Material[renderer.materials.Length];
            for (int i = 0; i < renderer.materials.Length; i++) {
                temporalMaterial[i] = transparentMaterial;
            }
            renderer.materials = temporalMaterial;
        }
    }
}
