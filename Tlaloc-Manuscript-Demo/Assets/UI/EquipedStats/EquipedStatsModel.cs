using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipedStatsModel : MonoBehaviour
{
    [SerializeField] private EquipmentController equipmentController;

    public int minAttack;
    public int maxAttack;

    public int strengthBonus;
    public int intelligenceBonus;
    public int hpBonus;
    public int manaBonus;
    public int attackPowerBonus;
    public int spellPowerBonus;

    public void UpdateEquipedStats() {

        strengthBonus = 0;
        intelligenceBonus = 0;
        hpBonus = 0;
        manaBonus = 0;
        attackPowerBonus = 0;
        spellPowerBonus = 0;

        WeaponItem[] equipedItems = new WeaponItem[3];

        if (equipmentController.leftWeaponRef != null) {
            equipedItems[0] = (WeaponItem)equipmentController.leftWeaponRef;
        }

        if (equipmentController.rightWeaponRef != null) {
            equipedItems[1] = (WeaponItem)equipmentController.rightWeaponRef;
        }

        if (equipmentController.bookRef != null) {
            equipedItems[2] = (WeaponItem)equipmentController.bookRef;
        }

        foreach (WeaponItem item in equipedItems) {
            if (item != null) {
                strengthBonus += item.strengthBonus;
                intelligenceBonus += item.strengthBonus;
                hpBonus += item.hpBonus;
                manaBonus += item.manaBonus;
                attackPowerBonus += item.attackPowerBonus;
                spellPowerBonus += item.spellPowerInt;
            }
        }
    }
    
}
