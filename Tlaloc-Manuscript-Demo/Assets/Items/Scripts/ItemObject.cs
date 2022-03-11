using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(BoxCollider))]
public class ItemObject : MonoBehaviour
{
    public Item item;
    public int amount = 1;


}
