using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsView : MonoBehaviour
{
    [SerializeField] private StatsModel model;

    public TextMeshPro level;
    public TextMeshPro exp;
    public TextMeshPro hp;
    public TextMeshPro mana;
    public TextMeshPro strength;
    public TextMeshPro intelligence;
    public TextMeshPro armor;
    public TextMeshPro attackPower;
    public TextMeshPro spellPower;
    public TextMeshPro defence;

    private void Awake() {
        model.OnValueChange += UpdateStatsOnUI;
    }

    private void UpdateStatsOnUI() { 
    
    }
}
