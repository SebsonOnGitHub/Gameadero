using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static float trocaCollected;
    public static int capsCollected;
    public float landSpeed;
    public Rigidbody rb;
    public float growthScalar;
    public float weightScalar;
    public bool respawning;

    protected bool isGrounded;
    protected float currSpeed;
    protected float swimmingSpeed;

    private Vector3 respawnPos;
    private Quaternion respawnRot;
    private Vector3 smallestSize;
    private float smallestMass;

    void Start() {
        Init();
    }

    public virtual void Init() {
        rb = GetComponent<Rigidbody>();
        respawning = false;
        isGrounded = false;
        trocaCollected = 0;
        capsCollected = 0;
        smallestSize = gameObject.transform.localScale;
        smallestMass = rb.mass;
        currSpeed = landSpeed;
        swimmingSpeed = currSpeed / 2;
    }

    public void Update() {
        isGrounded = IsGrounded();

        if (FindObjectOfType<MainCamera>().InPlace()) {
            respawning = false;
        }

        if (respawning) {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else {
            rb.constraints = RigidbodyConstraints.None;
        }
    }

    public void OnCollisionEnter(Collision collision) {
        Respawning(collision);
    }

    public void Respawning(Collision collision) {
        if (collision.collider.CompareTag("Respawn")) {
            respawnPos = transform.position;
            respawnRot = transform.rotation;
        }
    }

    public virtual bool IsGrounded() {
        if (!Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1f)) {
            return false;
        }
        else {
            return true;
        }
    }

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("DeathPlane")) {
            respawning = true;
            transform.position = respawnPos;
            transform.rotation = respawnRot;
            rb.velocity = Vector3.zero;
        }
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
}
