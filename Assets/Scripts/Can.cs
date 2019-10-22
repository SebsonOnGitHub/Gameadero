using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can : Collectable
{
    public int size;
    public float currentChange;
    public float changeNeeded;

    private Vector3 smallestSize;
    private float smallestMass;
    private bool collecting;
    private float changeSpeed;

    public override void Init() {
        base.Init();
        size = 1;
        currentChange = 0;
        changeNeeded = 0;
        smallestSize = transform.localScale;
        smallestMass = 1000;
        collecting = false;
        changeSpeed = 0.04f;
    }

    private void Update() {
        UpdateSize();
    }

    private void UpdateSize() {
        if (Mathf.Abs(changeNeeded) > 0.001f) {
            currentChange += changeSpeed * Mathf.Sign(changeNeeded);
            changeNeeded -= changeSpeed * Mathf.Sign(changeNeeded);
        }
        else {
            currentChange = size - 1;
            changeNeeded = 0;
        }
        transform.localScale = smallestSize + smallestSize * currentChange;
        GetComponent<Rigidbody>().mass = smallestMass + smallestMass * currentChange;
    }

    public void OnTriggerEnter(Collider other) {
        if (!collecting && (other.CompareTag("PlayerBall") || other.CompareTag("DeathPlane")) && 
            PlayerController.currState != PlayerController.State.FILLING_CAN && currentChange == (int)currentChange) {

            collecting = true;
            audioObject.Collect(this);
            FindObjectOfType<PlayerBall>().CollectCan(size);

            Destroy(gameObject);
        }
    }

    public void ChangeSize(int sizeChange) {
        changeNeeded += sizeChange;
        size += sizeChange;
    }
}
