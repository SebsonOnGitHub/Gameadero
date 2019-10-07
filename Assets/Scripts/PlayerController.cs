using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 jump;
    private bool isGrounded;
    public float speed;
    Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 6f, 0.0f)*rb.mass;
    }

    void OnCollisionStay(Collision collision) {
        if (collision.collider is TerrainCollider)
            isGrounded = true;
    }
    void OnCollisionExit(Collision collision) {
        if (collision.collider is TerrainCollider)
            isGrounded = false;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            rb.AddForce(jump, ForceMode.Impulse);
            isGrounded = false;
        }

        Vector3 movement = new Vector3(speed*moveHorizontal, 0, speed*moveVertical);

        GetComponent<Rigidbody>().AddForce(movement);
    }
}
