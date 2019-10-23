using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingCan : MonoBehaviour
{
    public GameObject DodgeCourse;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool hittingPlayer = false;
    private Vector3 speed;

    private void Start() {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        speed = new Vector3(0, 0, 10000);
    }

    private void Update(){
        GetComponent<Rigidbody>().AddForce(speed);

        if (hittingPlayer) {
            FindObjectOfType<GameMaster>().currPlayer.rb.AddForce(new Vector3(0, 10, 50));
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("PlayerMan") || collision.collider.CompareTag("PlayerBall")) {
            hittingPlayer = true;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.collider.CompareTag("PlayerMan") || collision.collider.CompareTag("PlayerBall")) {
            hittingPlayer = false;
        }
    }

    public void SpawnAgain() {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
