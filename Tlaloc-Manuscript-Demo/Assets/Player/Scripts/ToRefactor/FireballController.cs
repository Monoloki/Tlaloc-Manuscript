using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour {

    [SerializeField] private MagicBookController magicBookController;
    [SerializeField] private GameObject explosion;

    public void onActive() {
        magicBookController = FindObjectOfType<MagicBookController>();
    }

    public void onDeactive() {

        magicBookController.createdFireBall = null;
        magicBookController.CreateFireBall();

        StartCoroutine(Throw());
    }

    IEnumerator Throw() {

        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<SphereCollider>().isTrigger = false;

        transform.parent = null;
        yield return new WaitForSeconds(4f);
        Instantiate(explosion, transform).transform.parent = null;
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision) {

        var _explosion = Instantiate(explosion, transform);
        _explosion.transform.localPosition = new Vector3(0,0,0);
        _explosion.transform.parent = null;


        Destroy(gameObject);
    }
}