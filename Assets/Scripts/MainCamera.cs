using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private GameMaster gameMaster;
    private Vector3 vecToPlayer;

    public void Start() {
        Init();
    }

    public void Init() {
        gameMaster = FindObjectOfType<GameMaster>();
        vecToPlayer = new Vector3(0, 8, -13);
    }

    public void Update() {
        FollowPlayer();
    }

    public void FollowPlayer() {
        transform.position = gameMaster.currPlayer.transform.position + vecToPlayer;
    }
}
