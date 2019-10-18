using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCube : MonoBehaviour
{
    private bool manInWater;
    private float buoyancy;

    public void Start() {
        Init();
    }

    public void Init() {
        manInWater = false;
        buoyancy = 120;
    }

    public void Update() {
        Player player = FindObjectOfType<GameMaster>().currPlayer.GetComponent<PlayerMan>();

        if (manInWater && player) {
            player.GetComponent<Rigidbody>().AddForce(new Vector3(0, (player.GetComponent<Rigidbody>().mass * 0.1f) * buoyancy, 0));
        }
    }

    public void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("PlayerMan")) {
            manInWater = true;
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.CompareTag("PlayerMan")) {
            manInWater = false;
        }
    }
}
