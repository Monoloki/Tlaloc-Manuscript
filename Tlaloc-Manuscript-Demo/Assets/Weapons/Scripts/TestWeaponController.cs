using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeaponController : MonoBehaviour
{
    public int damage = 20;
    [SerializeField] private WeaponItem itemRef;
    
    [SerializeField] private BoxCollider boxCollider;

    public EquipedStatsModel equipedModel;
    public StatsModel statsModel;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy") {
            damage = Random.Range(itemRef.minAttack, itemRef.maxAttack);
            damage += equipedModel.attackPowerBonus + statsModel.attackPower;
            other.GetComponent<TestEnemy>().ApplyDamage(damage);
        }
    }

    public void SetBoxColliderTrigger(bool state) {
        boxCollider.isTrigger = state;   
    }

}
