using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class StatsModel : MonoBehaviour
{
    private int maxLevel = 30;

    public Action OnValueChange;

    public bool reachedMaxLevel = false;

    public int level { get; private set; }
    public int exp {
        get => exp;
        set {
            exp = value;

            if (!reachedMaxLevel) {               
                var _level = Mathf.FloorToInt(value / 100);
                if (_level >= maxLevel) {
                    reachedMaxLevel = true;
                    level = maxLevel;
                }
                else {
                    level = _level;
                }
            }       
            
            OnValueChange?.Invoke();
        } 
    }
    public int maxHP {
        get => maxHP;
        set { OnValueChange?.Invoke(); } 
    }
    public int maxMana {
        get => maxMana;  
        set { OnValueChange?.Invoke(); }
    }
    public int strength { 
        get => strength;
        set {
            OnValueChange?.Invoke();
        } 
    }
public int intelligence {
        get => intelligence;
        set {
            OnValueChange?.Invoke();
        } 
    }
    public int armor {
        get => armor;
        set { armor = value;
            if (armor >= 4000) {
                defence = 40;
            }
            else {
                defence = Mathf.Sqrt(armor) / 1.57f;
            }           
            OnValueChange?.Invoke();
        } 
    }
    public int attackPower { get; private set; }
    public int spellPower { get; private set; }
    public float defence { get; private set; }
}
