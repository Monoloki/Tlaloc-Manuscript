using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Containter", menuName = "Towers/Tower Containter")]
public class TowerInventory : ScriptableObject
{
    public Tower[] container;
}
