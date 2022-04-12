using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Items/Weapon")]
public class WeaponItem : Item {

    public int minAttack;
    public int maxAttack;

    public int strengthBonus;
    public int intelligenceBonus;
    public int hpBonus;
    public int manaBonus;
    public int attackPowerBonus;
    public int spellPowerInt;
}
