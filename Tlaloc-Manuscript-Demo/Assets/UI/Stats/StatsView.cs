using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsView : MonoBehaviour
{
    [SerializeField] private StatsModel model;

    [SerializeField] private TMP_Text level;
    [SerializeField] private TMP_Text exp;
    [SerializeField] private TMP_Text hp;
    [SerializeField] private TMP_Text mana;
    [SerializeField] private TMP_Text strength;
    [SerializeField] private TMP_Text intelligence;
    [SerializeField] private TMP_Text armor;
    [SerializeField] private TMP_Text attackPower;
    [SerializeField] private TMP_Text spellPower;
    [SerializeField] private TMP_Text defence;

    private void Awake() {
        model.OnValueChange += UpdateStatsOnUI;
    }

    private void UpdateStatsOnUI() {
        level.text = $"Level:{model.level}";
        exp.text = $"Experience:{model.exp}/{(model.level+1) * 100}";
        hp.text = $"HP:{model.maxHP}";
        mana.text = $"Mana:{model.maxMana}";
        strength.text = $" Strength:{model.strength}";
        intelligence.text = $" Intelligence:{model.intelligence}";
        armor.text = $" Armor:{model.armor}";
        attackPower.text = $" Attack Power:{model.attackPower}";
        spellPower.text = $" Spell Power:{model.spellPower}";
        defence.text = $" Defence:{model.defence}%";
    }

    private void OnDestroy() {
        model.OnValueChange -= UpdateStatsOnUI;
    }
}
