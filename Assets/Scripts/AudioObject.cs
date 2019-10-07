using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
    public AudioClip collectAudio;
    public AudioSource audioSource;

    void Start() {
        audioSource.clip = collectAudio;
    }

    public void CollectCan(){
        audioSource.Play();
    }
}
