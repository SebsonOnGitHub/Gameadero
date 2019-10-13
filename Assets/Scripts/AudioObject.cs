using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
    public AudioClip collectAudio;
    public AudioSource audioSource;

    void Start() {
    }

    public void CollectCan() {
        audioSource.clip = collectAudio;
        audioSource.Play();
    }

    public void CollectCap() {
        audioSource.clip = collectAudio;
        audioSource.Play();
    }
}
