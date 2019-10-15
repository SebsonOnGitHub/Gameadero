using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMan : Player
{
    private Vector3 jumpVec;
    private bool isGrounded;

    void FixedUpdate() {
        if (!Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1f)) {
            isGrounded = false;
        }
        else {
            isGrounded = true;
        }
    }

    public override void Jump() {
        jumpVec = new Vector3(0.0f, 2f, 0.0f);
        if (isGrounded) {
            rb.AddForce(jumpVec, ForceMode.Impulse);
        }
    }

    public void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Water") {
            FindObjectOfType<GameMaster>().SwitchMode();
        }
    }

    public void CollectCap() {
        FindObjectOfType<GameMaster>().capsInWorld--;
        capsCollected += 1;
    }
}
