using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class BuildingUiController : MonoBehaviour
{

    [Header("Building Menu")]

    [SerializeField] private GameObject buildingMenu;

    [SerializeField] private Image buildingImage;

    [SerializeField] private Sprite[] buildingPNG;

    [SerializeField] private TMP_Text buildingName;

    [Header("Actions")]

    public InputActionReference spawnMosnters = null;

    private BuildingSystem buildingSystem;

    private int activePNG = 0;

    [Header("Opening Portal")]

    [SerializeField] private GameObject portal;

    [SerializeField] private GameObject portalLight;

    [SerializeField] private Light gameLight;

    [Header("Monster Spawner")]

    [SerializeField] private GameObject monsterToSpawn;

    [SerializeField] private GameObject spawner;

    [SerializeField] private GameObject spawnEffect;

    [SerializeField] private Transform[] pointToWalk;

    [Header("Tower")]

    [SerializeField] private GameObject tower;

    int click = 0;

    [Header("In Seconds")] public float timeToSetNight = 3.5f;

    private void Awake() {
        buildingSystem = FindObjectOfType<BuildingSystem>();
        spawnMosnters.action.started += SpawnMonsters;

        UpdateUi();

        buildingSystem.objectToInstantiate = tower;

        buildingSystem.startShowingBuilding = true;
    }

    private void SpawnMonsters(InputAction.CallbackContext context) {
        click++;

        switch (click) {
            case 1:
                StartCoroutine("Spawn");
                break;
            default:
                break;
        }

        
    }


    IEnumerator Spawn() {

        for (int i = 0; i < 10; i++) {

            Instantiate(spawnEffect, spawner.transform);
            var monster = Instantiate(monsterToSpawn, spawner.transform);
            //monster.GetComponent<MonsterMoving>().pointToWalk = pointToWalk;

            yield return new WaitForSeconds(2f);
        }
    }

    private void ActivePortal(InputAction.CallbackContext context) {

        click++;

        switch (click) {
            case 1:
                StartCoroutine("SetNightLight");
                break;
            case 2:
                portal.SetActive(true);
                break;
            case 3:
                portalLight.SetActive(true);
                break;
            default:
                break;
        }
    }

    IEnumerator SetNightLight() {

        for (int i = 0; i < timeToSetNight * 10; i++) {

            RenderSettings.ambientIntensity -= 0.02f;
            gameLight.intensity -= 0.02f;

            yield return new WaitForSeconds(0.1f);
        }
        
    }

    private void ActiveBuildingWindow(InputAction.CallbackContext context) {
        buildingMenu.SetActive(!buildingMenu.activeSelf);
        buildingSystem.startShowingBuilding = buildingMenu.activeSelf;
    }

    public void OnClickNextButton() {
        activePNG++;
        if (activePNG >= buildingPNG.Length) {
            activePNG = 0;
        }

        UpdateUi();
    }

    public void OnClickCancelButton() {
        buildingSystem.startShowingBuilding = false;
    }

    public void OnClickChooseButton() {
        buildingSystem.objectToInstantiate = buildingSystem.buildingArray[activePNG];
        buildingSystem.startShowingBuilding = true;
    }

    public void OnClickPreviousButton() {
        activePNG--;
        if (activePNG < 0) {
            activePNG = buildingPNG.Length - 1;
        }

        UpdateUi();
    }

    private void UpdateUi() {
        UpdatePNG();
        UpdateText();
    }

    private void UpdatePNG() {
        buildingImage.sprite = buildingPNG[activePNG];
    }

    private void UpdateText() {
        buildingName.text = buildingPNG[activePNG].name;
    }
}
