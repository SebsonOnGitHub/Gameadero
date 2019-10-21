using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {
    public GameObject doorText;
    public GameObject doorCap;
    public GameObject doorLeft;
    public GameObject doorRight;
    public float unlockCount;
    public float gateReachDist;

    private bool locked;

    public void Start() {
        Init();
    }

    public void Init() {
        locked = true;
    }

    public void Update() {
        PlayerNearby();

        if (locked) {
            doorText.GetComponent<TextMesh>().text = unlockCount.ToString();
            ForceDoors(40);
        }
        else {
            ForceDoors(-40);
        }
    }

    public void PlayerNearby() {
        Player player = FindObjectOfType<GameMaster>().currPlayer;
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist <= gateReachDist && Player.capsCollected >= unlockCount && locked) {
            Unlock();
        }
    }

    public void Unlock() {
        locked = false;
        Vector3 openForce = new Vector3(0, 0, -100);
        doorLeft.GetComponent<Rigidbody>().AddRelativeForce(openForce, ForceMode.Impulse);
        doorRight.GetComponent<Rigidbody>().AddRelativeForce(openForce, ForceMode.Impulse);

        Destroy(doorText);
        Destroy(doorCap);
    }

    public void ForceDoors(int force) {
        Vector3 openForce = new Vector3(0, 0, force);
        doorLeft.GetComponent<Rigidbody>().AddRelativeForce(openForce);
        doorRight.GetComponent<Rigidbody>().AddRelativeForce(openForce);
    }
}
