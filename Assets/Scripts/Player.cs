using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 smallestSize;
    private float smallestMass;

    public bool emptying = false;
    public static float trocaCollected;
    public float speed;
    public Rigidbody rb;
    public bool ballMode;
    public float growthScalar;
    public float weightScalar;
    public Collectable canPrefab;

    void Start() {
        rb = GetComponent<Rigidbody>();
        trocaCollected = 0;
        smallestSize = gameObject.transform.localScale;
        smallestMass = rb.mass;
    }

    public void Move() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(speed * moveHorizontal, 0, speed * moveVertical);
        rb.AddForce(movement);
    }

    public void SetSize() {
        float newSize = trocaCollected * growthScalar;
        gameObject.transform.localScale = new Vector3(newSize, newSize, newSize) + smallestSize;
        rb.mass = (trocaCollected * weightScalar) + smallestMass;
    }

    public virtual void Jump() {}
    public virtual void CreateCan() {}
    public virtual void AdjustCan() {}
    public virtual Bowl BowlNearby() {return null;}
    public virtual void FillBowl(Bowl bowl) {}
    public virtual void AdjustBowl(Bowl bowl) { }
}
