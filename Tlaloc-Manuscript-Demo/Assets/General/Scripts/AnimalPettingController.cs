using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalPettingController : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip pettingSound;

    private void OnTriggerEnter(Collider other) {
        PlayPettingSound();
    }

    private void PlayPettingSound() {
        if (!source.isPlaying) {
            source.clip = pettingSound;
            source.Play();
            source.clip = null;
        }  
    }
}
