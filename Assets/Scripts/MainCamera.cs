using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Vector3 vecToPlayer;
    public Vector3 wantedPos;

    public void Start() {
        Init();
    }

    public void Init() {
        vecToPlayer = new Vector3(0, 8, -13);
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
