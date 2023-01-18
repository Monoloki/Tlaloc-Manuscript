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
    [SerializeField] private TMP_Text label;

    private void Awake() {
        UpdateUiLabel();
    }

    public void StartWave() {
        if (!isCurrentlyRunningWave) {     
            StartCoroutine(SpawnMonsters(activeLevelWaveData.waveData[activeWave].monsterList));
            activeWave++;
            UpdateUiLabel();
        }
    }

    public void LeaveArena() {
        if (!isCurrentlyRunningWave) {
            ClearMonsters();
            TeleportPlayerToHub();
            BuildingSystemSetup();
            StartCoroutine(FindObjectOfType<LevelController>().UnloadLevelScenes());
        }
    }

    private void BuildingSystemSetup() {
        var buildingUiController = FindObjectOfType<BuildingUiController>();
        var buildingSystem = FindObjectOfType<BuildingSystem>();
        //TODO: usuniêcie zespawnowanych potworów

        foreach (var tower in buildingSystem.spawnedTowers) {
            Destroy(tower);
        }
        buildingSystem.spawnedTowers = new List<GameObject>();
        buildingSystem.towerBuildingMode = false;
        buildingUiController.towerBuildingMode = false;
        buildingUiController.ResetActive();  
    }

    public void TeleportPlayerToHub() {
        player.transform.position = HubSpawn.position;
    }

    public void UpdateUiLabel() {
        label.text = $"Wave: {activeWave}/{activeLevelWaveData.waveData.Length}";
    }

    private IEnumerator SpawnMonsters(GameObject[] montersToSpawn) {
        isCurrentlyRunningWave = true;
        for (int i = 0; i < montersToSpawn.Length; i++) {
            Vector3 position = enemySpawner.transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            var enemie = Instantiate(montersToSpawn[i],position,Quaternion.identity);
            var navigator = enemie.GetComponent<EnemyStateController>();
            navigator.waypointArray = waypointArray;
            aliveEnemiesOfCurrentWave.Add(enemie);
            yield return new WaitForSeconds(monsterSpawnDelay);
        }
        yield return new WaitForEndOfFrame();
        isCurrentlyRunningWave = false;
    }

    private void ClearMonsters() {
        foreach (var enemy in aliveEnemiesOfCurrentWave) {
            Destroy(enemy);
        }
    }

}
