using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSpawn : MonoBehaviour
{
    
    [Header("Updated in awake")]
    public Transform HubSpawn;
    public GameObject player;
    [Header("Public properties")]
    public Transform[] waypointArray;
    public Wave activeLevelWaveData;

    private int activeWave = 0;
    private bool isCurrentlyRunningWave = false;

    [SerializeField] private GameObject enemySpawner;
    [SerializeField] private float monsterSpawnDelay = 2f;
    [SerializeField] private List<GameObject> aliveEnemiesOfCurrentWave;

    
    
    public void StartWave() {
        if (!isCurrentlyRunningWave) {     
            StartCoroutine(SpawnMonsters(activeLevelWaveData.waveData[activeWave].monsterList));
            activeWave++;
        }
    }

    public void LeaveArena() {
        if (!isCurrentlyRunningWave) {       
            StartCoroutine(FindObjectOfType<LevelController>().UnloadLevelScenes());
            TeleportPlayerToHub();

            BuildingSystemSetup();
        }
    }

    private void BuildingSystemSetup() {
        var buildingUiController = FindObjectOfType<BuildingUiController>();
        var buildingSystem = FindObjectOfType<BuildingSystem>();
        buildingSystem.towerBuildingMode = false;
        buildingUiController.towerBuildingMode = false;
        buildingUiController.ResetActive();
        //TODO: usuniêcie zespawnowanych potworów
        foreach (var tower in buildingSystem.spawnedTowers) {
            Destroy(tower.gameObject);
        }
    }

    public void TeleportPlayerToHub() {
        player.transform.position = HubSpawn.position;
    }

    public void UpdateUiLabel(TMP_Text label) {
        label.text = $"Wave: {activeWave + 1}/{activeLevelWaveData.waveData.Length}";
    }

    private IEnumerator SpawnMonsters(GameObject[] montersToSpawn) {
        for (int i = 0; i < montersToSpawn.Length; i++) {

            Vector3 position = enemySpawner.transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            var enemie = Instantiate(montersToSpawn[i],position,Quaternion.identity);
            var navigator = enemie.GetComponent<EnemyStateController>();
            navigator.waypointArray = waypointArray;
            aliveEnemiesOfCurrentWave.Add(enemie);
            yield return new WaitForSeconds(monsterSpawnDelay);
        }
        StartCoroutine(WaveSupervisor());
    }

    private IEnumerator WaveSupervisor() {
        isCurrentlyRunningWave = true;
        while (aliveEnemiesOfCurrentWave != null) {
            yield return new WaitForEndOfFrame();
        }
        isCurrentlyRunningWave = false;
    }

}
