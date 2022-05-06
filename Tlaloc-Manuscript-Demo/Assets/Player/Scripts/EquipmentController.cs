using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentSlot {
    leftWeapon = 0,
    rightWeapon = 1,
    leftTool = 2,
    rightTool = 3,
    book = 4,
}

public class EquipmentController : MonoBehaviour
{
    [Header("Parents of items")]
    [SerializeField] private GameObject leftWeaponGrabable;
    [SerializeField] private GameObject rightWeaponGrabable;
    [SerializeField] private GameObject leftToolGrabable;
    [SerializeField] private GameObject rightToolGrabable;
    [SerializeField] private GameObject bookGrabable;

    [Header("Equiped items")]
    public GameObject leftWeaponEquiped;
    public GameObject rightWeaponEquiped;
    public GameObject leftToolEquiped;
    public GameObject rightToolEquiped;
    public GameObject bookEquiped;

    [Header("Equiped items ref")]
    public Item leftWeaponRef;
    public Item rightWeaponRef;
    public Item bookRef;

    [Header("Stats Refs")]
    public EquipedStatsModel equipedModel;
    public StatsModel statsModel;

    [Header("Settings")]
    [SerializeField] private float timeToRestore = 2f;

    //TODO: following camera rotation with degree 

    public void EquipItem(EquipmentSlot slot, GameObject itemToInstantiate) {
        switch (slot) {
            case EquipmentSlot.leftWeapon:
                if (leftWeaponEquiped != null) Destroy(leftWeaponEquiped);
                leftWeaponEquiped = Instantiate(itemToInstantiate, leftWeaponGrabable.transform);
                leftWeaponEquiped.transform.localPosition = Vector3.zero;

                var leftWeaponController = leftWeaponEquiped.GetComponent<TestWeaponController>();
                leftWeaponController.equipedModel = equipedModel;
                leftWeaponController.statsModel = statsModel;

                break;
            case EquipmentSlot.rightWeapon:
                if (rightWeaponEquiped != null) Destroy(rightWeaponEquiped);
                rightWeaponEquiped = Instantiate(itemToInstantiate, rightWeaponGrabable.transform);
                rightWeaponEquiped.transform.localPosition = Vector3.zero;

                var rightWeaponController = leftWeaponEquiped.GetComponent<TestWeaponController>();
                rightWeaponController.equipedModel = equipedModel;
                rightWeaponController.statsModel = statsModel;

                break;
            case EquipmentSlot.leftTool:
                if (leftToolEquiped != null) Destroy(leftToolEquiped);
                leftToolEquiped = Instantiate(itemToInstantiate, leftToolGrabable.transform);
                leftToolEquiped.transform.localPosition = Vector3.zero;
                break;
            case EquipmentSlot.rightTool:
                if (rightToolEquiped != null) Destroy(rightToolEquiped);
                rightToolEquiped = Instantiate(itemToInstantiate, rightToolGrabable.transform);
                rightToolEquiped.transform.localPosition = Vector3.zero;
                break;
            case EquipmentSlot.book:
                if (bookEquiped != null) Destroy(bookEquiped);
                bookEquiped = Instantiate(itemToInstantiate, bookGrabable.transform);
                bookEquiped.transform.localPosition = Vector3.zero;

                var bookWeaponController = leftWeaponEquiped.GetComponent<TestWeaponController>();
                bookWeaponController.equipedModel = equipedModel;
                bookWeaponController.statsModel = statsModel;

                break;
            default:
                break;
        }
    }

    public void EquipItem(EquipmentSlot slot, GameObject itemToInstantiate, Item itemRef) {
        switch (slot) {
            case EquipmentSlot.leftWeapon:
                if (leftWeaponEquiped != null) Destroy(leftWeaponEquiped);
                leftWeaponEquiped = Instantiate(itemToInstantiate, leftWeaponGrabable.transform);
                leftWeaponEquiped.transform.localPosition = Vector3.zero;
                leftWeaponRef = itemRef;
                break;
            case EquipmentSlot.rightWeapon:
                if (rightWeaponEquiped != null) Destroy(rightWeaponEquiped);
                rightWeaponEquiped = Instantiate(itemToInstantiate, rightWeaponGrabable.transform);
                rightWeaponEquiped.transform.localPosition = Vector3.zero;
                rightWeaponRef = itemRef;
                break;
            case EquipmentSlot.leftTool:
                if (leftToolEquiped != null) Destroy(leftToolEquiped);
                leftToolEquiped = Instantiate(itemToInstantiate, leftToolGrabable.transform);
                leftToolEquiped.transform.localPosition = Vector3.zero;
                break;
            case EquipmentSlot.rightTool:
                if (rightToolEquiped != null) Destroy(rightToolEquiped);
                rightToolEquiped = Instantiate(itemToInstantiate, rightToolGrabable.transform);
                rightToolEquiped.transform.localPosition = Vector3.zero;
                break;
            case EquipmentSlot.book:
                if (bookEquiped != null) Destroy(bookEquiped);
                bookEquiped = Instantiate(itemToInstantiate, bookGrabable.transform);
                bookEquiped.transform.localPosition = Vector3.zero;
                bookRef = itemRef;
                break;
            default:
                break;
        }

    }

    //TODO: change later
    public void UpdateWeaponDmg(GameObject InstantiatedWeapon) {
        var test = InstantiatedWeapon.GetComponent<TestWeaponController>();
        
    } 

    #region Basic equipment logic

    public void OnGrabEquipment(Rigidbody grabableRb) {
        grabableRb.isKinematic = true;
        grabableRb.useGravity = false;
        grabableRb.gameObject.GetComponent<Collider>().isTrigger = false;
    }

    public void OnRealeseEquipment(Rigidbody grabableRb) {
        grabableRb.isKinematic = false;
        grabableRb.useGravity = true;
        StartCoroutine(RestoringGrabable(grabableRb));
    }

    private IEnumerator RestoringGrabable(Rigidbody grabableRb) {
        yield return new WaitForSeconds(timeToRestore);
        grabableRb.gameObject.GetComponent<Collider>().isTrigger = true;
        grabableRb.isKinematic = true;
        grabableRb.useGravity = false;
        grabableRb.transform.localPosition = Vector3.zero;
        grabableRb.transform.rotation = Quaternion.identity;
    }
    #endregion
}
