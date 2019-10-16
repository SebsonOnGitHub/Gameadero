using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
    public AudioClip canAudio;
    public AudioClip capAudio;
    public AudioSource audioSource;

    public void Collect(Collectable collectable) {
        if (collectable is Can) {
            audioSource.clip = canAudio;
        }
        else if (collectable is Cap) {
            audioSource.clip = capAudio;
        }

        audioSource.Play();
    }
}
