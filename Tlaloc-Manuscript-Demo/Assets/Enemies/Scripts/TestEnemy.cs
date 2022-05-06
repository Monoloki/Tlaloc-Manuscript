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

    private void Awake() {
        playerPosition = FindObjectOfType<Camera>().transform;
        currentHP = maxHP;
        UpdateSlider();
    }

    private void Update() {
        hpBar.transform.LookAt(playerPosition);
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
}
