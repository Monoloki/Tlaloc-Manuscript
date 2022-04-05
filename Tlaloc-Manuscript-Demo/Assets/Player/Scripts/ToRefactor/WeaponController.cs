using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponController : MonoBehaviour{
    [SerializeField] private GameObject weapon1;
    [SerializeField] private GameObject weapon2;
    [SerializeField] private GameObject weapon3;
    [SerializeField] private float lerpTime = 3;

    [SerializeField] private GameObject hand1;
    [SerializeField] private GameObject hand2;
    [SerializeField] private GameObject hand3;

    private Rigidbody weapon1Rigidbody;
    private Rigidbody weapon2Rigidbody;
    private Rigidbody weapon3Rigidbody;

    private BoxCollider weapon1MeshCollider;
    private BoxCollider weapon2MeshCollider;
    private BoxCollider weapon3MeshCollider;

    //private bool weapon1Reset = false;
    //private bool weapon2Reset = false;
    //private bool weapon3Reset = false;

    [SerializeField] Transform cameraToFollow;
    [SerializeField] Transform controllerCenter;

    private void Start(){
        weapon1Rigidbody = weapon1.GetComponent<Rigidbody>();
        weapon1MeshCollider = weapon1.GetComponent<BoxCollider>();

        weapon2Rigidbody = weapon2.GetComponent<Rigidbody>();
        weapon2MeshCollider = weapon2.GetComponent<BoxCollider>();

        weapon3Rigidbody = weapon3.GetComponent<Rigidbody>();
        weapon3MeshCollider = weapon3.GetComponent<BoxCollider>();
    }

    private void Update(){
        controllerCenter.position = cameraToFollow.position;

        controllerCenter.rotation = Quaternion.Lerp(Quaternion.Euler(0, cameraToFollow.rotation.eulerAngles.y, 0), Quaternion.Euler(0, controllerCenter.rotation.eulerAngles.y, 0), Time.deltaTime * lerpTime);

    }

    public void OnActiveWeapon1(){
        weapon1MeshCollider.isTrigger = false;
        weapon1Rigidbody.useGravity = true;
        weapon1Rigidbody.isKinematic = false;

        StopCoroutine(RespawnWeapon1());
    }

    public void OnActiveWeapon2(){
        weapon2MeshCollider.isTrigger = false;
        weapon2Rigidbody.useGravity = true;
        weapon2Rigidbody.isKinematic = false;

        StopCoroutine(RespawnWeapon2());
    }

    public void OnActiveWeapon3() {
        weapon3MeshCollider.isTrigger = false;
        weapon3Rigidbody.useGravity = true;
        weapon3Rigidbody.isKinematic = false;

        StopCoroutine(RespawnWeapon3());
    }

    public void OnDeactiveWeapon1(){
        StartCoroutine(RespawnWeapon1());
    }

    public void OnDeactiveWeapon2(){
        StartCoroutine(RespawnWeapon2());
    }

    public void OnDeactiveWeapon3() {
        StartCoroutine(RespawnWeapon3());
    }

    IEnumerator RespawnWeapon3() {
        weapon3Rigidbody.useGravity = true;
        weapon3Rigidbody.isKinematic = false;

        weapon3.transform.parent = null;

        yield return new WaitForSeconds(2f);

        weapon3.transform.parent = hand3.transform;

        weapon3MeshCollider.isTrigger = true;
        weapon3MeshCollider.enabled = false;
        weapon3.transform.localPosition = new Vector3(0.007f, (-0.3f), (-0.07f));
        weapon3.transform.rotation = Quaternion.Euler(new Vector3(42f, (-77.8f), 92.35f));
        weapon3MeshCollider.enabled = true;
        weapon3Rigidbody.useGravity = false;
        weapon3Rigidbody.isKinematic = true;
        //weapon3Reset = false;
    }

    IEnumerator RespawnWeapon1(){

        weapon1Rigidbody.useGravity = true;
        weapon1Rigidbody.isKinematic = false;

        weapon1.transform.parent = null;

        yield return new WaitForSeconds(3f);

        weapon1.transform.parent = hand1.transform;

        weapon1MeshCollider.isTrigger = true;
        weapon1MeshCollider.enabled = false;
        weapon1.transform.localPosition = new Vector3(0, -0.6f, 0);
        weapon1.transform.rotation = Quaternion.Euler(new Vector3(-90f, 0, 115.5f));
        weapon1MeshCollider.enabled = true;
        weapon1Rigidbody.useGravity = false;
        weapon1Rigidbody.isKinematic = true;
        //weapon1Reset = false;
    }

    IEnumerator RespawnWeapon2(){

        weapon2Rigidbody.useGravity = true;
        weapon2Rigidbody.isKinematic = false;

        weapon2.transform.parent = null;

        yield return new WaitForSeconds(3f);

        weapon2.transform.parent = hand2.transform;

        weapon2MeshCollider.isTrigger = true;
        weapon2MeshCollider.enabled = false;
        weapon2.transform.localPosition = new Vector3(0.086f, -0.6f, 0.057f);
        weapon2.transform.rotation = Quaternion.Euler(new Vector3(-90f, 0, 44f));
        weapon2MeshCollider.enabled = true;
        weapon2Rigidbody.useGravity = false;
        weapon2Rigidbody.isKinematic = true;
        //weapon2Reset = false;
    }
}
