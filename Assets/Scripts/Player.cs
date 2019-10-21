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
    public float currSpeed;
    public List<int> standingOn = new List<int>();

    protected float waterSpeed;
    protected float currRotateSpeed;
    protected Vector3 respawnPos;
    protected bool isGrounded = false;
    protected bool isSwimming = false;
    protected List<int> swimmingIn = new List<int>();
    protected List<int> onCourseBars = new List<int>();

    private Vector3 smallestSize;
    private float smallestMass;


    void Start() {
        Init();
    }

    public virtual void Init() {
        rb = GetComponent<Rigidbody>();
        respawning = false;
        trocaCollected = 0;
        capsCollected = 0;
        smallestSize = gameObject.transform.localScale;
        smallestMass = rb.mass;
        currSpeed = landSpeed;
        waterSpeed = 0.6f * landSpeed;
        currRotateSpeed = 1.6f;
    }

    public void Update() {
        UpdatingPlayer();
    }

    public virtual void UpdatingPlayer() {
        if (FindObjectOfType<MainCamera>().InPlace()) {
            respawning = false;
        }

        if (onCourseBars.Count < 2 && GetComponent<PlayerBall>()) {
            currSpeed = landSpeed;
        }
        else if (onCourseBars.Count == 2 && GetComponent<PlayerBall>()) {
            currSpeed = 10;
        }

        if (swimmingIn.Count > 0) {
            isSwimming = true;
        }
        else {
            isSwimming = false;
        }

        if (standingOn.Count > 0) {
            isGrounded = true;
        }
        else {
            isGrounded = false;
        }

        if (respawning) {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else if (rb.constraints == RigidbodyConstraints.FreezeAll) {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        Respawning(collision);
        OnCourse(collision);
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.collider.CompareTag("CourseBar")) {
            onCourseBars.Remove(collision.collider.GetInstanceID());
        }

        if (onCourseBars.Count < 2 && GetComponent<PlayerBall>()) {
            currSpeed = landSpeed;
        }
    }

    public void Respawning(Collision collision) {
        if (collision.collider.CompareTag("Respawn")) {
            respawnPos = transform.position;
        }
    }

    public void OnCourse(Collision collision) {
        if (collision.collider.CompareTag("CourseBar")) {
            onCourseBars.Add(collision.collider.GetInstanceID());
        }

        if (onCourseBars.Count > 0 && GetComponent<PlayerMan>()) {
            transform.position = transform.position - new Vector3(0, 1.5f, 0);
        }
    }

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Water")) {
            swimmingIn.Add(other.GetInstanceID());
            if (swimmingIn.Count > 0) {
                currSpeed = waterSpeed;
            }
        }

        if (other.CompareTag("DeathPlane")) {
            respawning = true;
            transform.position = respawnPos;
            rb.velocity = Vector3.zero;
        }
        else if (!other.isTrigger && !other.CompareTag("PlayerMan") && !other.CompareTag("PlayerBall")) {
            standingOn.Add(other.GetInstanceID());
            if (standingOn.Count > 0) {
                isGrounded = true;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Water")) {
            swimmingIn.Remove(other.GetInstanceID());
            if (swimmingIn.Count == 0) {
                currSpeed = landSpeed;
            }
        }

        if (!other.isTrigger && standingOn.Contains(other.GetInstanceID())) {
            standingOn.Remove(other.GetInstanceID());
            if (standingOn.Count == 0) {
                isGrounded = false;
            }
        }
    }

    public void Move(int dir) {
        Vector3 moveForce = dir * FindObjectOfType<MainCamera>().forwardVec * currSpeed;

        if (isGrounded) {
            rb.AddForce(moveForce * 2.8f);
        }
        else {
            rb.AddForce(moveForce);
        }
    }

    public void Turn(int rot) {
        FindObjectOfType<PlayerMan>().transform.Rotate(new Vector3(0, rot * currRotateSpeed * 2, 0), Space.Self);
        FindObjectOfType<PlayerBall>().transform.Rotate(new Vector3(0, rot * currRotateSpeed * 2, 0), Space.Self);
        FindObjectOfType<MainCamera>().RotateAroundPlayer(rot * currRotateSpeed);
    }


    public void SetSize() {
        float newSize = trocaCollected * growthScalar;
        gameObject.transform.localScale = new Vector3(newSize, newSize, newSize) + smallestSize;
        rb.mass = (trocaCollected * weightScalar) + smallestMass;
    }
}
