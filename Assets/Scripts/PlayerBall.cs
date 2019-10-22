using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : Player
{
    public float bowlReachDist;
    public Can canPrefab;

    private Can spitCan;
    private int spitSize = 1;
    private int firstTime;

    public void CollectCan(int size) {
        trocaCollected += size;
        ChangeSize(size);
    }

    public void InitCan() {
        if (Player.changeNeeded == 0 && trocaCollected > 0) {
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

            Vector3 canOffset = FindObjectOfType<MainCamera>().forwardVec * (transform.localScale.z * 2);
            spitCan = Instantiate<Can>(canPrefab, transform.position + canOffset, canPrefab.transform.rotation);
            firstTime = 1;

            PlayerController.currState = PlayerController.State.FILLING_CAN;
        }
        else {
            PlayerController.currState = PlayerController.State.NONE;
        }
    }

    public void FillCan() {
        if (Player.changeNeeded == 0) {
            if (Input.GetKey(PlayerController.keyAction) && trocaCollected > 0) {
                trocaCollected -= spitSize;
                ChangeSize(-spitSize);
                spitCan.ChangeSize(spitSize - firstTime);

                if (firstTime == 1) {
                    firstTime = 0;
                }
            }
            else {
                spitCan = null;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                PlayerController.currState = PlayerController.State.NONE;
            }
        }
    }

    public Bowl BowlNearby() {
        List<Bowl> bowls = new List<Bowl>(FindObjectsOfType<Bowl>());

        foreach (Bowl bowl in bowls) {
            if (Vector3.Distance(bowl.transform.position, transform.position) <= bowlReachDist && bowl.percentFullness < 1) {
                return bowl;
            }
        }

        return null;
    }

    public void InitBowl(Bowl bowl) {
        if (Player.changeNeeded == 0 && trocaCollected > 0) {
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

            PlayerController.currState = PlayerController.State.FILLING_BOWL;
        }
        else {
            PlayerController.currState = PlayerController.State.NONE;
        }
    }

    public void FillBowl(Bowl bowl) {
        if (Player.changeNeeded == 0) {
            if (Input.GetKey(PlayerController.keyAction) && bowl && trocaCollected > 0) {
                trocaCollected -= spitSize;
                ChangeSize(-spitSize);
                bowl.ChangeFullness(spitSize);
            }
            else {
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                PlayerController.currState = PlayerController.State.NONE;
            }
        }
    }
}
