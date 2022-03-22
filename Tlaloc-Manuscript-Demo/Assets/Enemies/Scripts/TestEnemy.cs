using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestEnemy : MonoBehaviour
{
    [SerializeField] private int maxHP;
    [SerializeField] private int currentHP;
    [SerializeField] private Slider hPBar;
    public Transform playerPosition;

    private void Awake() {
        currentHP = maxHP;
        UpdateSlider();
    }

    private void Update() {
        hPBar.transform.LookAt(playerPosition);
    }

    public void UpdateHP(int newMaxHP) {
        currentHP = maxHP = newMaxHP;
        UpdateSlider();
    }

    public void ApplyDamage(int damage) {
        currentHP -= damage;
        hPBar.value = currentHP;

        if (currentHP <= 0) {
            Debug.Log($"{gameObject} has died");
            Destroy(gameObject);
        }
    }

    public void UpdateSlider() {
        hPBar.maxValue = maxHP;
        hPBar.value = currentHP;
    }
}
