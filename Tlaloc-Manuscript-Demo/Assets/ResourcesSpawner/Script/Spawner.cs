using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject recoursePrefab;
    [SerializeField] private GameObject[] spawnedResources;
    [SerializeField] private int amountToSpawn = 10;
    [SerializeField] private BoxCollider spawnCollider;

    private void Awake() {
        Bounds bounds = spawnCollider.bounds;
        spawnedResources = new GameObject[amountToSpawn];

        for (int i = 0; i < amountToSpawn; i++) {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float z = Random.Range(bounds.min.z, bounds.max.z);
            spawnedResources[i] = Instantiate(recoursePrefab, transform);
            spawnedResources[i].transform.eulerAngles = new Vector3(0, Random.Range(0,359), 0);
            spawnedResources[i].transform.position = new Vector3(x, 0, z);
        }
    }
}
