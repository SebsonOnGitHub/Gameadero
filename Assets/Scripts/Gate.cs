using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public GameObject door;

    public float unlockCount;
    public float gateReachDist;

    void Start() {
        gateReachDist = 5;
    }

    void Update() {
        PlayerNearby();
    }

    public void PlayerNearby() {
        Player player = FindObjectOfType<GameMaster>().currPlayer;

        if (Vector3.Distance(player.transform.position, transform.position) <= gateReachDist && unlockCount == Player.capsCollected) {
            Unlock();
        }
    }

    public void Unlock() {
        Destroy(door);
    }
}
