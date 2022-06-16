using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Tower", menuName = "Towers/Tower")]
public class Tower : ScriptableObject
{
    public GameObject Prefab;
    public Sprite icon;
    public int price;
}
