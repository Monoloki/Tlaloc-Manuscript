using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquipedStatsView : MonoBehaviour
{
    [SerializeField] private EquipedStatsModel model;

    [SerializeField] private TMP_Text hp;
    [SerializeField] private TMP_Text mana;
    [SerializeField] private TMP_Text strength;
    [SerializeField] private TMP_Text intelligence;
    [SerializeField] private TMP_Text attackPower;
    [SerializeField] private TMP_Text spellPower;

    public void UpdateStatsOnUI() {

        hp.text = $"HP Bonus: {model.hpBonus}";
        mana.text = $"Mana Bonus: {model.manaBonus}";
        strength.text = $" Strength Bonus: {model.strengthBonus}";
        intelligence.text = $" Intelligence Bonus: {model.intelligenceBonus}";
        attackPower.text = $" Attack Power Bonus: {model.attackPowerBonus}";
        spellPower.text = $" Spell Power Bonus: {model.spellPowerBonus}";
    }
}
