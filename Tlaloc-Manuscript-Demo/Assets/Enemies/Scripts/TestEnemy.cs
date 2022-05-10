using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestEnemy : MonoBehaviour
{
    [SerializeField] private int maxHP;
    [SerializeField] private int currentHP;
    [SerializeField] private Slider hpBar;
    [SerializeField] private int expToGive;
    public Transform playerPosition;

    [SerializeField] private Loot[] lootTable;

    private void Awake() {
        playerPosition = FindObjectOfType<Camera>().transform;
        currentHP = maxHP;
        UpdateSlider();
    }

    private void Update() {
        hpBar.transform.LookAt(playerPosition);
    }

    private void OnDestroy() {
        DropLoot();
    }

    public void UpdateHP(int newMaxHP) {
        currentHP = maxHP = newMaxHP;
        UpdateSlider();
    }

    public void ApplyDamage(int damage) {
        currentHP -= damage;
        hpBar.value = currentHP;

        if (currentHP <= 0) {
            Debug.Log($"{gameObject} has died");
            Destroy(gameObject);
            GivePlayerExp();
        }
    }

    public void UpdateSlider() {
        hpBar.maxValue = maxHP;
        hpBar.value = currentHP;
    }

    private void GivePlayerExp() {
        FindObjectOfType<StatsModel>().exp += expToGive; 
    }

    private void DropLoot() {
        foreach (Loot itemToLoot in lootTable) {
            if (Random.Range(0,100) <= itemToLoot.chanceToDrop) {
                var rb = Instantiate(itemToLoot.item, transform.position + Vector3.up * 5, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(new Vector3(Random.Range(-4,4),7, Random.Range(-4, 4)), ForceMode.Impulse);
            }
        }
    }

    [System.Serializable]
    public class Loot {
        public GameObject item;
        public float chanceToDrop;
    }
}
