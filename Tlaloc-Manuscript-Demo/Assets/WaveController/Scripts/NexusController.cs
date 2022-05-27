using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusController : MonoBehaviour
{
    public int hp = 10;
    [SerializeField] private LevelSpawn levelSpawn;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Enemy") {        
            hp--;
            Destroy(other.gameObject);
            if (hp <= 0) {
                levelSpawn.StopAllCoroutines();
                levelSpawn.TeleportPlayerToHub();              
                StartCoroutine(FindObjectOfType<LevelController>().UnloadLevelScenes());
            }
        }
    }
}
