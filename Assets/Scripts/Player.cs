using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 smallestSize;
    private float smallestMass;

    protected float currSpeed;
    protected float swimmingSpeed;

    public bool emptying = false;
    public bool creatingCan = false;
    public bool fillingBowl = false;
    public bool swimming = false;
    public static float trocaCollected;
    public float landSpeed;
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
        currSpeed = landSpeed;
        swimmingSpeed = currSpeed / 2;
    }

    public void Move() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(currSpeed * moveHorizontal, 0, currSpeed * moveVertical);
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
