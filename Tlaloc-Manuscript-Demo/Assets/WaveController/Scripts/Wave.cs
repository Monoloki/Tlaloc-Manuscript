using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Wave")]
public class Wave : ScriptableObject
{
    [System.Serializable]
    public class WaveNumer {
        public 
            GameObject[] monsterList;
    }

    public WaveNumer[] waveData;
}
