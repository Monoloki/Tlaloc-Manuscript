using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    #region singleton
    private static LevelController _instance;
    public static LevelController Instance { get { return _instance; } }
    #endregion

    public List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    public List<int> loadedScenes = new List<int>();

    [SerializeField] private MeshRenderer portal;

    public Transform activeSpawn;

    [SerializeField] private GameObject player;

    [SerializeField] private Material loadingMaterial;

    [SerializeField] private Material doneLoadingMaterial;

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        }
        else {
            _instance = this;
        }
    }

    public void LoadSceneAsync(int index) {

        scenesToLoad.Add(SceneManager.LoadSceneAsync(index,LoadSceneMode.Additive));
        StartCoroutine(LoadingScene(index));

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
        }
        yield return new WaitForEndOfFrame();
        var levelspawn = FindObjectsOfType<LevelSpawn>();
        activeSpawn = levelspawn[levelspawn.Length - 1].transform;
        

        if (!loadedScenes.Contains(index)) {
            loadedScenes.Add(index);
        }

    }

    IEnumerator UnloadAllScenesExpectOne(int index) {
        for (int i = 0; i < loadedScenes.Count; i++) {
            if (loadedScenes[i] != index) {
                SceneManager.UnloadSceneAsync(loadedScenes[i]);
                yield return new WaitForEndOfFrame();
            }           
        }
    
    }

    public void TeleportPlayerToActiveSpawn() {
        player.transform.position = activeSpawn.position;
    }
}
