using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cap : Collectable
{
    public void Update(){
        transform.Rotate(Vector3.up, Space.World);
    }

    public void OnCollisionEnter(Collision collision) {
        PlayerMan pMan = collision.collider.GetComponent<PlayerMan>();
        if (pMan) {
            audioObject.Collect(this);
            pMan.CollectCap();
            Destroy(gameObject);
        }
    }
}
