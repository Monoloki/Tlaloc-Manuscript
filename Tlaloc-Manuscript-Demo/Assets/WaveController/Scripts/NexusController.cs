using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusController : MonoBehaviour
{
    public int hp = 10;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Enemy") {        
            hp--;
            Destroy(other.gameObject);
        }
    }
}
