using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    public List<int> loadedScenes = new List<int>();
    public Transform activeSpawn;
    public Transform hubSpawn;

    [SerializeField] private WaveController waveController;
    [SerializeField] private MeshRenderer portal;
    [SerializeField] private GameObject player;
    [SerializeField] private Material loadingMaterial;
    [SerializeField] private Material doneLoadingMaterial;

    private int levelToLoad = 0;
    private int activeLevelIndex = 0;

    public void SetLevelToLoad(int levelSceneIndex) {
        levelToLoad = levelSceneIndex;
    }

    public void LoadLevelAsync() {
        scenesToLoad.Add(SceneManager.LoadSceneAsync(levelToLoad, LoadSceneMode.Additive));
        StartCoroutine(UnloadScenesLoadedWhilePlaying());

    }

    IEnumerator LoadingScene(int index) {
        float progress = 0;
        for (int i = 0; i < scenesToLoad.Count; ++i) {
            portal.material = loadingMaterial;
            while (!scenesToLoad[i].isDone) {
                progress += scenesToLoad[i].progress;
                yield return null;
            }
            portal.material = doneLoadingMaterial;
            activeLevelIndex = index;
            waveController.activeLevelIndex = activeLevelIndex;
        }
        yield return new WaitForEndOfFrame();
        var levelspawn = FindObjectOfType<LevelSpawn>();
        activeSpawn = levelspawn.transform;
        levelspawn.HubSpawn = hubSpawn;
        levelspawn.player = player;
        waveController.activeLevelSpawn = levelspawn;
        waveController.activeSpawner = activeSpawn.gameObject;
        

        TeleportPlayerToActiveSpawn();

        if (!loadedScenes.Contains(index)) {
            loadedScenes.Add(index);
        }
    }

    IEnumerator UnloadScenesLoadedWhilePlaying() {
        activeLevelIndex = 0;
        for (int i = 0; i < loadedScenes.Count; i++) {
            SceneManager.UnloadSceneAsync(loadedScenes[i]);
            loadedScenes.Remove(loadedScenes[i]);
            yield return new WaitForEndOfFrame();               
        }
        StartCoroutine(LoadingScene(levelToLoad));
    }

    public IEnumerator UnloadLevelScenes() {
        activeLevelIndex = 0;
        for (int i = 0; i < loadedScenes.Count; i++) {
            SceneManager.UnloadSceneAsync(loadedScenes[i]);
            loadedScenes.Remove(loadedScenes[i]);
            yield return new WaitForEndOfFrame();
        }
    }

    public void TeleportPlayerToActiveSpawn() {
        player.transform.position = activeSpawn.position;
    }
}
