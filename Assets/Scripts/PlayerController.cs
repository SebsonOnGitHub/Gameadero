using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum State { NONE, SWITCHING, SWIMMING, FLOATING, GLIDING, INIT_BOWL, FILLING_BOWL, ADJUSTING_BOWL, INIT_CAN, FILLING_CAN, ADJUSTING_CAN };
    public static State currState;

    public static KeyCode keyUp = KeyCode.W;
    public static KeyCode keyDown = KeyCode.S;
    public static KeyCode keyLeft = KeyCode.A;
    public static KeyCode keyRight = KeyCode.D;
    public static KeyCode keySwitch = KeyCode.V;
    public static KeyCode keyAction = KeyCode.C;
    public static KeyCode keyJump = KeyCode.Space;

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


        if (Input.GetKey(keyUp)) {
            gameMaster.currPlayer.Move(1);
        }

        if (Input.GetKey(keyLeft)) {
            gameMaster.currPlayer.Turn(-1);
        }

        if (Input.GetKey(keyRight)) {
            gameMaster.currPlayer.Turn(1);
        }

        if (Input.GetKey(keyDown)) {
            gameMaster.currPlayer.Move(-1);
        }

        if (Input.GetKeyDown(keyJump) && isMan) {
            gameMaster.currPlayer.GetComponent<PlayerMan>().Jump(false);
        }

        if (Input.GetKey(keyJump) && isMan) {
            gameMaster.currPlayer.GetComponent<PlayerMan>().Jump(true);
        }

        if (currState == State.NONE) { 
            if (Input.GetKeyDown(keySwitch)) {
                return State.SWITCHING;
            }
            else if (Input.GetKeyDown(keyAction) && isMan) {
                return State.GLIDING;
            }
            else if (Input.GetKeyDown(keyAction) && gameMaster.currPlayer.GetComponent<PlayerBall>().BowlNearby()) {
                return State.INIT_BOWL;
            }
            else if (Input.GetKeyDown(keyAction)) {
                return State.INIT_CAN;
            }
        }

        return currState;
    }
}
