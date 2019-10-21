using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject forwardKeeper;
    public Vector3 wantedPos;
    public Vector3 forwardVec;
    public Quaternion rotation;

    private Vector3 vecToPlayer;

    public void Start() {
        Init();
    }

    public void Init() {
        vecToPlayer = new Vector3(0, 10, -12);
        rotation = transform.rotation;
        transform.position = FindObjectOfType<GameMaster>().currPlayer.transform.position + vecToPlayer;
        wantedPos = transform.position;

        forwardKeeper.transform.rotation = new Quaternion(0, 0, 0, 1);
        forwardVec = forwardKeeper.transform.forward;
    }

    public void Update() {
        UpdateWantedPos();
        if (!InPlace()) {
            transform.position = Vector3.MoveTowards(transform.position, wantedPos, 0.6f);
        }
    }

    public void UpdateWantedPos() {
        wantedPos = FindObjectOfType<GameMaster>().currPlayer.transform.position + vecToPlayer;
    }

    public bool InPlace() {
        return transform.position == wantedPos;
    }

    public void RotateAroundPlayer(float rotation) {
        transform.RotateAround(FindObjectOfType<GameMaster>().currPlayer.transform.position, Vector3.up, 2 * rotation);
        vecToPlayer = Quaternion.Euler(0, 2 * rotation, 0) * vecToPlayer;
        forwardVec = forwardKeeper.transform.forward;
    }
}
