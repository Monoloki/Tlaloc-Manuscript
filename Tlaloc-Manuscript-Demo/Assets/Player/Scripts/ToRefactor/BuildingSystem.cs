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
    [SerializeField] private float maxDistance;
    [SerializeField] private Material transparentMaterial;
    [SerializeField] private Material errorTransparentMaterial;
    [SerializeField] private PlayerManager playerManager;

    private RaycastHit _hit;
    private Vector3 _direction;
    private Vector3 hitPositionValue;
    private bool isHitValue;
    private bool isErrorShowing = false;

    [HideInInspector] public GameObject objectToInstantiate;
    [HideInInspector] public BuildingsStats activeBuilding;

    public BuildingsStats[] buildingArray;

    public bool isObjectInstantiated = false;
    public InputActionReference spawnBuilding = null;
    public bool showBuildingPreview = false;
    public GameObject instantiatedBuilding;

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
        if (isObjectInstantiated) {
            SpawnBuilding();
        }
    }

    private void SpawnBuilding() {
        if (BuildRequirementsCheck(activeBuilding)) {
            //TODO: substract materials from inventory
            Instantiate(objectToInstantiate, instantiatedBuilding.transform.position, instantiatedBuilding.transform.rotation);
            Destroy(instantiatedBuilding);
            isObjectInstantiated = false;
            showBuildingPreview = false;
        }
        else if (isErrorShowing) {
            StartCoroutine(ErrorNotEnaughtMats(instantiatedBuilding));
        }
        
    }

    public void SpawnBuildingPreview() {

        if (!isObjectInstantiated) {
            instantiatedBuilding = Instantiate(objectToInstantiate, hitPositionValue, objectToInstantiate.transform.rotation);
            foreach (MeshCollider meshCollider in instantiatedBuilding.GetComponentsInChildren<MeshCollider>()) {
                meshCollider.enabled = false;
            }

            foreach (Renderer renderer in instantiatedBuilding.GetComponentsInChildren<Renderer>()) {
                Material[] temporalMaterial = new Material[renderer.materials.Length];
                for (int i = 0; i < renderer.materials.Length; i++) {
                    temporalMaterial[i] = transparentMaterial;
                }
                renderer.materials = temporalMaterial;    
            }

            isObjectInstantiated = true;
        }
    }

    private void ShowBuilding(){
        if (isHitValue && isObjectInstantiated) {
            instantiatedBuilding.transform.position = hitPositionValue;
        }
    }

    private void GetRaycastHit(){
        _direction = Vector3.Normalize(targetDirection.position - origin.position);
        if (Physics.Raycast(origin.position, _direction, out _hit, maxDistance,layerMask)){
            Debug.DrawRay(origin.position, _direction, Color.blue);
            hitPositionValue = _hit.point;
            isHitValue = true;
        }
        else{
            Debug.DrawRay(origin.position, _direction, Color.yellow);
            isHitValue = false;
        }

    }

    private bool BuildRequirementsCheck(BuildingsStats statsToCheck) {
        bool requireMet = false;
        foreach (var requirement in statsToCheck.requirements) {
            foreach (var item in playerManager.playerInventory.container) {
                if (item.item == requirement.material && item.amount >= requirement.requireAmount) {
                    //playerManager.playerInventory.RemoveItem(requirement.material.name, requirement.requireAmount);
                    //item.amount -= requirement.requireAmount;
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

    private IEnumerator ErrorNotEnaughtMats(GameObject previewObject) {
        isErrorShowing = true;
        var renderers = instantiatedBuilding.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers) {
            Material[] temporalMaterial = new Material[renderer.materials.Length];
            for (int i = 0; i < renderer.materials.Length; i++) {
                temporalMaterial[i] = errorTransparentMaterial;
            }
            renderer.materials = temporalMaterial;
        }
        yield return new WaitForSeconds(10);

        foreach (Renderer renderer in renderers) {
            Material[] temporalMaterial = new Material[renderer.materials.Length];
            for (int i = 0; i < renderer.materials.Length; i++) {
                temporalMaterial[i] = transparentMaterial;
            }
            renderer.materials = temporalMaterial;
        }
        isErrorShowing = false;
    }
}
