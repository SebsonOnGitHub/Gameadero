using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMan : Player
{
    private Vector3 jumpVec;
    private bool isGrounded;

    public override void Jump() {
        jumpVec = new Vector3(0.0f, 12f, 0.0f) * rb.mass;
        if (isGrounded) {
            rb.AddForce(jumpVec, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionStay(Collision collision) {
        if (collision.collider is TerrainCollider)
            isGrounded = true;
    }

    void OnCollisionExit(Collision collision) {
        if (collision.collider is TerrainCollider)
            isGrounded = false;
    }
}
