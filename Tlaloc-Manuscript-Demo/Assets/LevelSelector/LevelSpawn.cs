using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawn : MonoBehaviour
{
    public Transform[] waypointArray;

    [SerializeField] private GameObject enemySpawner;

    public Wave activeWaveData;

    [SerializeField] private float monsterSpawnDelay = 2f;

    private int activeWave = 0;

    private void SpawnWave(int waveNumber) {

        foreach (GameObject monster in activeWaveData.waveData[waveNumber].monsterList) {

        }
    }

    private IEnumerator SpawnMonsters(GameObject[] montersToSpawn) {
        for (int i = 0; i < montersToSpawn.Length; i++) {

            Vector3 position = enemySpawner.transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));

            var navigator = Instantiate(montersToSpawn[i],position,Quaternion.identity).GetComponent<EnemyStateController>();

            navigator.waypointArray = waypointArray;

            yield return new WaitForSeconds(monsterSpawnDelay);
        }
    }

    //To test

    private void Awake() {
        StartCoroutine(SpawnMonsters(activeWaveData.waveData[activeWave].monsterList));
    }
}
