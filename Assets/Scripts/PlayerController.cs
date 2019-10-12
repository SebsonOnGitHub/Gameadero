﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameMaster gameMaster;

    void Start() {
    }

    void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.V)) {
            gameMaster.SwitchMode();
        }
        else if ((Player.trocaCollected >= 1 && Input.GetKey(KeyCode.C)) || gameMaster.currPlayer.spitting) {
            if (Input.GetKey(KeyCode.C)) {
                gameMaster.currPlayer.CreateCan();
            }
            else {
                gameMaster.currPlayer.AdjustCan();
            }
        }
        else if (Input.GetKey(KeyCode.W) ||
                 Input.GetKey(KeyCode.A) ||
                 Input.GetKey(KeyCode.S) ||
                 Input.GetKey(KeyCode.D)) {
            gameMaster.currPlayer.Move();

            if (Input.GetKeyDown(KeyCode.Space)) {
                gameMaster.currPlayer.Jump();
            }
        }
    }

}
