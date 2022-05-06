using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {
    [Header("Reference Updated after Level Loaded")]
    public GameObject activeSpawner;
    public int activeLevelIndex;
    public LevelSpawn activeLevelSpawn;

    [Space(10)]
    [SerializeField] private Wave[] levelsWaveReference;
   
}
