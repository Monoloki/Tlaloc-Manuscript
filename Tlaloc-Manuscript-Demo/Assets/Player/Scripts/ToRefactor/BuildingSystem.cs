using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class BuildingSystem : MonoBehaviour {
    public Vector3 hitPositionValue;
    public bool isHitValue;

    [SerializeField] private Transform origin;
    [SerializeField] private Transform targetDirection;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float maxDistance;

    [HideInInspector] public GameObject objectToInstantiate;
    [SerializeField] private Material transparentMaterial;

    private GameObject instantiatedBuilding;

    public GameObject[] buildingArray;

    private RaycastHit _hit;
    private Vector3 _direction;

    public bool clicked = false;

    private bool isObjectInstantiated = false;

    public InputActionReference toggleReference = null;

    public bool startShowingBuilding = false;

    [SerializeField] private GameObject[] monsters;

    private void Awake() {
        toggleReference.action.started += ToggleFunction;     
    }

    private void Update(){ 
        if (clicked) {
            GetRaycastHit();
            ShowBuilding();
        }
    }

    private void ToggleFunction(InputAction.CallbackContext context) {
        if (startShowingBuilding) {
            clicked = !clicked;

            if (isObjectInstantiated) {
                Instantiate(objectToInstantiate, instantiatedBuilding.transform.position, instantiatedBuilding.transform.rotation);

                Destroy(instantiatedBuilding);
                isObjectInstantiated = false;
            }
            else {
                SpawnObject();
            }
        }           
    }

    private void SpawnObject() {

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

        /*
        if (isHitValue && isObjectInstantiated) {            
            instantiatedBuilding.transform.position = hitPositionValue;
        }
        else if (!isHitValue) {
            if (instantiatedBuilding != null) {
                Destroy(instantiatedBuilding);
                isObjectInstantiated = false;
            }        
        }      
        */
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
}
