using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    public GameObject capPrefab;
    public GameObject leftTrigger;
    public GameObject rightTrigger;

    private bool leftDown;
    private bool rightDown;

    public void Start() {
        Init();
    }

    public void Init() {
        leftDown = false;
        rightDown = false;
    }

    public void OnCollisionEnter(Collision collision) {
        if (collision.collider.gameObject == leftTrigger && !leftDown) {
            leftDown = true;
            rightDown = false;
        }
        else if (collision.collider.gameObject == rightTrigger && !rightDown) {
            rightDown = true;
            leftDown = false;
            SpawnObject(capPrefab, new Vector3(-43, 3, -4), capPrefab.transform.rotation, transform);
        }
    }

    public void SpawnObject(GameObject Object, Vector3 position, Quaternion rotation, Transform parent) {
        Instantiate(Object, position, rotation, parent);
    }
}
