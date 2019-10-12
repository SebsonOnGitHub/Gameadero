using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : Player
{
    private float spitSize = 0;
    private float spitSpeed = 0.05f;
    private Collectable spitCan;

    public void CollectCan(float size) {
        trocaCollected += size;
        SetSize();
    }

    public override void CreateCan() {
        if (!spitting) {
            spitting = true;
            spitSize = 0;
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
        trocaCollected = Mathf.Floor(trocaCollected);
        SetSize();
        spitCan.SetSize(spitSize);
        spitCan = null;
        rb.isKinematic = false;
        spitting = false;
    }

}
