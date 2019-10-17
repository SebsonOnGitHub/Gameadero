using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum State { NONE, SWITCHING, GLIDING, INIT_BOWL, FILLING_BOWL, ADJUSTING_BOWL, INIT_CAN, FILLING_CAN, ADJUSTING_CAN };
    public static State currState;
    public GameMaster gameMaster;

    public void Start() {
        Init();
    }

    public void Init() {
        currState = State.NONE;
    }

    public void Update() {
        currState = NextState();
        Bowl nearbyBowl;

        switch (currState) {
            case State.NONE:
                break;
            case State.SWITCHING:
                gameMaster.SwitchMode();
                break;
            case State.GLIDING:
                gameMaster.currPlayer.GetComponent<PlayerMan>().Glide();
                break;
            case State.INIT_BOWL:
                nearbyBowl = gameMaster.currPlayer.GetComponent<PlayerBall>().BowlNearby();
                gameMaster.currPlayer.GetComponent<PlayerBall>().InitBowl(nearbyBowl);
                break;
            case State.FILLING_BOWL:
                nearbyBowl = gameMaster.currPlayer.GetComponent<PlayerBall>().BowlNearby();
                gameMaster.currPlayer.GetComponent<PlayerBall>().FillBowl(nearbyBowl);
                break;
            case State.ADJUSTING_BOWL:
                nearbyBowl = gameMaster.currPlayer.GetComponent<PlayerBall>().BowlNearby();
                gameMaster.currPlayer.GetComponent<PlayerBall>().AdjustBowl(nearbyBowl);
                break;
            case State.INIT_CAN:
                gameMaster.currPlayer.GetComponent<PlayerBall>().InitCan();
                break;
            case State.FILLING_CAN:
                gameMaster.currPlayer.GetComponent<PlayerBall>().FillCan();
                break;
            case State.ADJUSTING_CAN:
                gameMaster.currPlayer.GetComponent<PlayerBall>().AdjustCan();
                break;
            default:
                Debug.Log("PlayerController is not in a recognizable state");
                break;
        }
    }

    private State NextState() {
        bool isMan = gameMaster.currPlayer.GetComponent<PlayerMan>();


        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
            gameMaster.currPlayer.Move();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isMan) {
            gameMaster.currPlayer.GetComponent<PlayerMan>().Jump();
        }

        if (currState == State.NONE) { 
            if (Input.GetKeyDown(KeyCode.V)) {
                return State.SWITCHING;
            }
            else if (Input.GetKeyDown(KeyCode.C) && isMan) {
                return State.GLIDING;
            }
            else if (Input.GetKeyDown(KeyCode.C) && gameMaster.currPlayer.GetComponent<PlayerBall>().BowlNearby()) {
                return State.INIT_BOWL;
            }
            else if (Input.GetKeyDown(KeyCode.C)) {
                return State.INIT_CAN;
            }
        }

        return currState;
    }
}
