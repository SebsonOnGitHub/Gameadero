using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Vector3 wantedPos;
    public Quaternion rotation;

    private Vector3 vecToPlayer;

    public void Start() {
        Init();
    }

    public void Init() {
        vecToPlayer = new Vector3(0, 8, -13);
        transform.Rotate(0, 0, 0, Space.World);
        //vecToPlayer = new Vector3(13, 8, 0);
        //transform.Rotate(0, -90, 0, Space.World);
        rotation = transform.rotation;
        transform.position = FindObjectOfType<GameMaster>().currPlayer.transform.position + vecToPlayer;
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
}
