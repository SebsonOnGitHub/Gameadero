using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private GameMaster gameMaster;
    private Vector3 vecToPlayer;

    void Start() {
        gameMaster = FindObjectOfType<GameMaster>();
        vecToPlayer = transform.position - gameMaster.currPlayer.transform.position;
    }

    void Update() {
        FollowPlayer();
    }

    public void FollowPlayer() {
        transform.position = gameMaster.currPlayer.transform.position + vecToPlayer;
    }
}
