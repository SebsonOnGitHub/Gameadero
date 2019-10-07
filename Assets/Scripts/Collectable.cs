using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public AudioObject audioObject;

    void OnCollisionEnter(Collision collision) {
        Player player = collision.collider.GetComponent<Player>();
        if (player) {
            audioObject.CollectCan();
            player.CollectCan();
            Destroy(gameObject);
        }
    }
}
