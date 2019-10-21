using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can : Collectable
{
    public float size;
    
    private float mass;
    private Vector3 smallestSize;
    private bool collecting;

    public override void Init() {
        base.Init();
        size = 1;
        mass = 1000;
        smallestSize = gameObject.transform.localScale;
        collecting = false;
    }

    public void OnTriggerEnter(Collider other) {
        if (!collecting && (other.CompareTag("PlayerBall") || other.CompareTag("DeathPlane")) && PlayerController.currState != PlayerController.State.FILLING_CAN) {
            collecting = true;
            audioObject.Collect(this);
            FindObjectOfType<PlayerBall>().CollectCan(size);
            FindObjectOfType<PlayerMan>().SetSize();

            Destroy(gameObject);
        }
    }

    public void SetSize(float canSize) {
        if (canSize == 0) {
            Destroy(gameObject);
        }

        size = canSize;
        Vector3 newSize = size * smallestSize;
        if (size < 1) {
            newSize = smallestSize;
        }
        gameObject.transform.localScale = newSize;
        gameObject.GetComponent<Rigidbody>().mass = mass * canSize;
    }
}
