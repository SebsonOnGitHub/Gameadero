using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public Player currPlayer;
    public Player secondPlayer;
    public int capsInWorld;

    public void Start() {
        Init();
    }

    public void Init() {
        Physics.gravity = new Vector3(0, -23F, 0);
        secondPlayer.rb.isKinematic = true;
    }

    public void SwitchMode() {
        Vector3 tempPos = new Vector3(0, -1100, 0);
        Vector3 currPos = currPlayer.transform.position;
        Vector3 secondPos = secondPlayer.transform.position;
        currPlayer.transform.position = tempPos;
        secondPlayer.transform.position = currPos;
        currPlayer.transform.position = secondPos;

        Player tempPlayer = currPlayer;
        currPlayer = secondPlayer;
        secondPlayer = tempPlayer;

        currPlayer.rb.isKinematic = false;
        secondPlayer.rb.isKinematic = true;

        PlayerController.currState = PlayerController.State.NONE;
    }
}
