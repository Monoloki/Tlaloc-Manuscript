using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    [SerializeField] private GameObject leftWeaponGrabable;
    [SerializeField] private GameObject rightWeaponGrabable;
    [SerializeField] private GameObject leftToolGrabable;
    [SerializeField] private GameObject rightToolGrabable;
    [SerializeField] private GameObject bookGrabable;

    [SerializeField] private float timeToRestore = 2f;

    //TODO: Respawning item after realising, setting gravity,collider to true after realising 

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
}
