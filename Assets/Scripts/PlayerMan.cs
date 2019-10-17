using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMan : Player
{
    public GameObject umbrellaPrefab;
    public float umbrellaFloat;

    private GameObject umbrella;
    private bool isGrounded;

    public void Update() {
        if (!Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1f)) {
            isGrounded = false;
        }
        else {
            isGrounded = true;
        }

        if (umbrella && rb.velocity.y <= 0) {
            rb.AddForce(new Vector3(0, (rb.mass * 0.1f) * umbrellaFloat, 0));
        }
    }

    public void Jump() {
        if (isGrounded) {
            rb.AddForce(new Vector3(0, 2, 0), ForceMode.Impulse);
        }
    }

    public void Glide() {
        if (!umbrella && !isGrounded) {
            Vector3 umbrellaPos = transform.position + new Vector3(0.4f, 0.4f, 0);
            umbrella = Instantiate(umbrellaPrefab, umbrellaPos, umbrellaPrefab.transform.rotation, transform);
        }
        else if (!Input.GetKey(KeyCode.C) || isGrounded) {
            Destroy(umbrella);
            PlayerController.currState = PlayerController.State.NONE;
        }
    }

    public void CollectCap() {
        FindObjectOfType<GameMaster>().capsInWorld--;
        capsCollected += 1;
    }
}
