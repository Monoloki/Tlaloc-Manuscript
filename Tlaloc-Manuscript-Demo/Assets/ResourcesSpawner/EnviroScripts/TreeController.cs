using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    [SerializeField] private TestEnemy.Loot[] lootTable;
    [SerializeField] private int durability;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.TryGetComponent<AxeToolController>(out AxeToolController axe)) {
            Debug.Log(other.gameObject + " Hit Tree");
            DropOnHit();
            durability--;
            if (durability <= 0) Destroy(gameObject);
        }
    }

    private void OnDestroy() {
        DropLoot();
    }

    private void DropLoot() {
        foreach (TestEnemy.Loot itemToLoot in lootTable) {
            if (Random.Range(0, 100) <= itemToLoot.chanceToDrop) {
                var rb = Instantiate(itemToLoot.item, transform.position + Vector3.up * 5, Quaternion.identity).GetComponent<Rigidbody>();
                rb.GetComponent<ItemObject>().amount = Random.Range(1, 3);
                rb.AddForce(new Vector3(Random.Range(-4, 4), 7, Random.Range(-4, 4)), ForceMode.Impulse);
            }
        }
    }

    private void DropOnHit() {
        var drop = Random.Range(0, lootTable.Length);
        var rb = Instantiate(lootTable[drop].item, transform.position + Vector3.up * 5, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(Random.Range(-4, 4), 7, Random.Range(-4, 4)), ForceMode.Impulse);
    }
}
