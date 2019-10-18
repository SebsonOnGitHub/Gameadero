using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : Player
{
    public float bowlReachDist;
    public Can canPrefab;

    private float spitSize = 0;
    private float spitSpeed = 0.05f;
    private Can spitCan;
    private float beforeSpitTroca;
    private float beforeSpitBowlFullness;

    public void CollectCan(float size) {
        trocaCollected += size;
        SetSize();
    }

    public void InitCan() {
        if (trocaCollected > 0) {
            spitSize = 0;
            beforeSpitTroca = trocaCollected;
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

            Vector3 canOffset = new Vector3(0, 0, (transform.localScale.z * 2));
            spitCan = Instantiate<Can>(canPrefab, transform.position + canOffset, canPrefab.transform.rotation);

            PlayerController.currState = PlayerController.State.FILLING_CAN;
        }
        else {
            PlayerController.currState = PlayerController.State.NONE;
        }
    }

    public void FillCan() {
        if (Input.GetKey(PlayerController.keyAction) && trocaCollected >= spitSpeed) {
            spitSize += spitSpeed;
            trocaCollected -= spitSpeed;
            SetSize();
            spitCan.SetSize(spitSize);
        }
        else {
            PlayerController.currState = PlayerController.State.ADJUSTING_CAN;
        }
    }

    public void AdjustCan() {
        spitSize = Mathf.Ceil(spitSize);
        trocaCollected = beforeSpitTroca - spitSize;
        SetSize();
        spitCan.SetSize(spitSize);
        spitCan = null;
        rb.constraints = RigidbodyConstraints.None;

        PlayerController.currState = PlayerController.State.NONE;
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
        if (trocaCollected > 0) {
            spitSize = 0;
            beforeSpitBowlFullness = bowl.currFullness;
            beforeSpitTroca = trocaCollected;
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

            PlayerController.currState = PlayerController.State.FILLING_BOWL;
        }
        else {
            PlayerController.currState = PlayerController.State.NONE;
        }
    }

    public void FillBowl(Bowl bowl) {
        if (Input.GetKey(PlayerController.keyAction) && bowl && trocaCollected >= spitSpeed && spitSize + spitSpeed <= bowl.maxFullness) {
            spitSize += spitSpeed;
            trocaCollected -= spitSpeed;
            SetSize();
            bowl.SetFullness(beforeSpitBowlFullness + spitSize);
        }
        else if (bowl) {
            PlayerController.currState = PlayerController.State.ADJUSTING_BOWL;
        }
        else {
            rb.constraints = RigidbodyConstraints.None;
            PlayerController.currState = PlayerController.State.NONE;
        }
    }

    public void AdjustBowl(Bowl bowl) {
        if (bowl.percentFullness < 1) {
            spitSize = Mathf.Ceil(spitSize);
            trocaCollected = beforeSpitTroca - spitSize;
            SetSize();
            bowl.SetFullness(beforeSpitBowlFullness + spitSize);
        }

        rb.constraints = RigidbodyConstraints.None;
        PlayerController.currState = PlayerController.State.NONE;
    }
}
