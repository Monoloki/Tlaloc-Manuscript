using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class StatsModel : MonoBehaviour
{
    private int maxLevel = 30;

    public Action OnValueChange;

    public Action OnLevelUp;

    public AnimationCurve animationCurve;
    public AnimationCurve attackPowerCurve;
    public AnimationCurve spellPowerCurve;
    public AnimationCurve experienceCurve;

    [HideInInspector]
    public bool reachedMaxLevel = false;

    public int level { get; private set; }
    public int exp {
        get => _exp;
        set {
            _exp = value;
            if (!reachedMaxLevel) {               
                var _level = Mathf.FloorToInt(value / 100);

                if (level < _level) OnLevelUp?.Invoke();

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

    private int _exp { get; set; }

    public int maxHP {
        get => _maxHP;
        set {
            _maxHP = value;
            OnValueChange?.Invoke(); 
        } 
    }

    private int _maxHP { get; set; }

    public int maxMana {
        get => _maxMana;  
        set {
            _maxMana = value;
            OnValueChange?.Invoke(); 
        }
    }

    private int _maxMana { get; set; }

    public int strength { 
        get => _strength;
        set {
            _strength = value;

            attackPower = _strength * 10;

            //attackPower = Mathf.RoundToInt(attackPowerCurve.Evaluate(_strength) * 10);

            OnValueChange?.Invoke();
        } 
    }

    private int _strength { get; set; }

    public int intelligence {
        get => _intelligence;
        set {
            _intelligence = value;

            spellPower = _intelligence * 10;

            //spellPower = Mathf.RoundToInt(spellPowerCurve.Evaluate(_intelligence) * 10); 

            OnValueChange?.Invoke();
        } 
    }

    private int _intelligence { get; set; }

    public int armor {
        get => _armor;
        set {
            _armor = value;
            if (_armor >= 4000) {
                defence = 40;
            }
            else {
                //defence = Mathf.Round(Mathf.Sqrt(_armor) / 1.57f);

                defence = Mathf.Round(animationCurve.Evaluate(_armor));
            }           
            OnValueChange?.Invoke();
        } 
    }

    private int _armor { get; set; }

    public int attackPower { get; private set; }
    public int spellPower { get; private set; }
    public float defence { get; private set; }
}
