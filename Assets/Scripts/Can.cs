using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can : Collectable
{
    private float size;
    private float mass;
    private Vector3 smallestSize;

    public override void Init() {
        base.Init();
        size = 1;
        mass = 1000;
        smallestSize = gameObject.transform.localScale;
    }

    public void OnCollisionEnter(Collision collision) {
        PlayerBall pBall = collision.collider.GetComponent<PlayerBall>();
        if (pBall && PlayerController.currState != PlayerController.State.FILLING_CAN) {
            audioObject.Collect(this);
            pBall.CollectCan(size);
            Destroy(gameObject);
        }
    }

    public void SetSize(float canSize) {
        size = canSize;
        Vector3 newSize = size * smallestSize;
        if (size < 1) {
            newSize = smallestSize;
        }
        gameObject.transform.localScale = newSize;
        gameObject.GetComponent<Rigidbody>().mass = mass * canSize;
    }
}
