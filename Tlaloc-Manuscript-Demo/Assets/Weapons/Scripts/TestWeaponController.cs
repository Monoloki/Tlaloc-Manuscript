using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeaponController : MonoBehaviour
{
    [SerializeField] private int damage = 20;
    [SerializeField] private BoxCollider boxCollider;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy") {
            other.GetComponent<TestEnemy>().ApplyDamage(damage);
        }
    }

    public void SetBoxColliderTrigger(bool state) {
        boxCollider.isTrigger = state;   
    }

}
