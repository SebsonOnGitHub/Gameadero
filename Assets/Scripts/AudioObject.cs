using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
    public AudioClip canAudio;
    public AudioClip orangeAudio;
    public AudioClip appleAudio;
    public AudioClip sugarAudio;
    public AudioClip waterAudio;
    public AudioClip loveAudio;
    public AudioSource audioSource;

    public void Collect(Collectable.CollectType type) {
        switch (type) {
            case Collectable.CollectType.CAN:
                audioSource.clip = canAudio;
                break;
            case Collectable.CollectType.ORANGE:
                audioSource.clip = orangeAudio;
                break;
            case Collectable.CollectType.APPLE:
                audioSource.clip = appleAudio;
                break;
            case Collectable.CollectType.SUGAR:
                audioSource.clip = sugarAudio;
                break;
            case Collectable.CollectType.WATER:
                audioSource.clip = waterAudio;
                break;
            case Collectable.CollectType.LOVE:
                audioSource.clip = loveAudio;
                break;
            default:
                Debug.Log("Collectable does not contain that CollectType");
                break;
        }

        audioSource.Play();
    }
}
