using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPresenter : MonoBehaviour
{
    public StatsModel model;
    public StatsView view;

    private void Start() {
        TestStatsAsign();
    }

    private void TestStatsAsign() {
        model.exp = 2000;
        model.maxHP = 1000;
        model.maxMana = 1000;
        model.strength = 10;
        model.intelligence = 10;
        model.armor = 3000;
    
    }

}
