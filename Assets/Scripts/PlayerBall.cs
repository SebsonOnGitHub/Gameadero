using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : Player
{
    private float spitSize = 0;
    private float spitSpeed = 0.05f;
    private Collectable spitCan;
    private float beforeSpitTroca;
    private float beforeSpitBowlFullness;

    public float bowlReactDist = 4;

    public void CollectCan(float size) {
        trocaCollected += size;
        SetSize();
    }

    public override void CreateCan() {
        if (!emptying) {
            emptying = true;
            spitSize = 0;
            beforeSpitTroca = trocaCollected;
            rb.isKinematic = true;
            spitCan = Instantiate<Collectable>(canPrefab);
            spitCan.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (transform.localScale.z * 2));
        }
        else if (trocaCollected >= spitSpeed) {
            rb.isKinematic = false;
            spitSize += spitSpeed;
            trocaCollected -= spitSpeed;
            SetSize();
            spitCan.SetSize(spitSize);
        }
    }

    public override void AdjustCan() {
        spitSize = Mathf.Ceil(spitSize);
        trocaCollected = beforeSpitTroca - spitSize;
        SetSize();
        spitCan.SetSize(spitSize);
        spitCan = null;
        rb.isKinematic = false;
        emptying = false;
    }
    
    public override Bowl BowlNearby() {
        List<Bowl> bowls = new List<Bowl>(FindObjectsOfType<Bowl>());

        foreach (Bowl bowl in bowls) {
            if (Vector3.Distance(bowl.transform.position, transform.position) <= bowlReactDist && bowl.currFullness != bowl.maxFullness) {
                return bowl;
            }
        }

        return null; 
    }

    public override void FillBowl(Bowl bowl) {
        if (!emptying) {
            emptying = true;
            spitSize = 0;
            beforeSpitBowlFullness = bowl.currFullness;
            beforeSpitTroca = trocaCollected;
            rb.isKinematic = true;
        }
        else if (trocaCollected >= spitSpeed && spitSize + spitSpeed <= bowl.maxFullness) {
            rb.isKinematic = false;
            spitSize += spitSpeed;
            trocaCollected -= spitSpeed;
            SetSize();
            bowl.SetFullness(beforeSpitBowlFullness + spitSize);
        }
    }

    public override void AdjustBowl(Bowl bowl) {
        spitSize = Mathf.Ceil(spitSize);
        trocaCollected = beforeSpitTroca - spitSize;
        SetSize();
        bowl.SetFullness(beforeSpitBowlFullness + spitSize);
        rb.isKinematic = false;
        emptying = false;
    }
}
