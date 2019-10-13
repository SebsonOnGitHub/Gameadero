using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private Vector3 smallestSize;

    public AudioObject audioObject;
    public float size;

    void Start() {
        audioObject = FindObjectOfType<AudioObject>();
        size = 1;
        smallestSize = gameObject.transform.localScale;
    }

    void OnCollisionEnter(Collision collision) {
        PlayerBall pBall = collision.collider.GetComponent<PlayerBall>();
        if (pBall && !pBall.creatingCan && GetComponentInParent<Cap>() == null) {
            audioObject.CollectCan();
            pBall.CollectCan(size);
            Destroy(gameObject);
        }

        PlayerMan pMan = collision.collider.GetComponent<PlayerMan>();
        if (pMan && GetComponentInParent<Cap>() != null) {
            audioObject.CollectCap();
            pMan.CollectCap();
            Destroy(GetComponentInParent<Cap>().gameObject);
        }
    }

    public void SetSize(float canSize) {
        size = canSize;
        Vector3 newSize = size * smallestSize;
        if (size < 1) {
            newSize = smallestSize;
        }
        gameObject.transform.localScale = newSize;
    }
}
